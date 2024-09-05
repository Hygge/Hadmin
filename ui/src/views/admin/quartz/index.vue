<script setup>
import {SearchOutlined, RedoOutlined, DeleteOutlined, FormOutlined, CaretRightOutlined, SlidersOutlined} from '@ant-design/icons-vue';
import {reactive, ref, onMounted, toRaw,  } from 'vue';
import {message} from "ant-design-vue";
import {AddJob, DelJob, GetList, StatusJob, UpdateJob} from "@/api/quartz.js";
import {useRouter} from "vue-router";


defineOptions({
  name : 'sysquartz'
})

onMounted(() => {

  search(null)

})

const query = reactive({
  jobName:'',
  category: '',
  status: undefined,
})
const reset = ()=>{
  query.jobName = '';
  query.category = '';
  query.category = '';
  query.status = undefined;
}

const search = (page) => {
  let form = {jobName: query.jobName, category: query.category, status: query.status, pageNum: paginationer.current,
    pageSize: paginationer.pageSize  };
  if (page !== null){
    form.pageSize = page.pageSize;
    form.pageNum = page.current;
  }
  GetList(form).then(res => {
    if (res.code === 200){
      let result = res.data
      dataSource.value = result.rows
      paginationer.total = result.total
      paginationer.current = result.pageNum
      paginationer.pageSize = result.pageSize
    }
  })

}

const route = useRouter()
const logTarget = ()=>{
  route.push('/quartz/log')
}

const selectRow = ref([])
const dataSource = ref([ ])
const columns= [
  {    title: '计划编号',    dataIndex: 'id',    key: 'id',    width: '10%',    align: 'center'  },
  {    title: '计划名称',    dataIndex: 'jobName',    key: 'jobName',    align: 'center'  },
  {    title: '计划类别',    dataIndex: 'category',    key: 'category',    align: 'center'  },
  {    title: '程序集名称',    dataIndex: 'assemblyName',    key: 'assemblyName',    align: 'center',  ellipsis: true, },
  {    title: '计划内容全类名',    dataIndex: 'typeName',    key: 'typeName',    align: 'center',  ellipsis: true,  },
  {    title: 'cron 表达式',    dataIndex: 'cronExpression',    key: 'cronExpression',    align: 'center'  },
  {    title: '并发执行',    dataIndex: 'concurrent',    key: 'concurrent',    align: 'center'  },
  {    title: '状态',    dataIndex: 'status',    key: 'status',    align: 'center'  },
  {    title: '执行周期',    dataIndex: 'second',    key: 'second',    align: 'center'  },
  {    title: '执行次数',    dataIndex: 'repeat',    key: 'repeat',    align: 'center'  },
]

const  paginationer = reactive({
  // 分页配置
  pageSize: 7, // 每页显示的条数
  showSizeChanger: true, // 是否可以改变每页显示的条数
  pageSizeOptions: ['10', '20', '30', '40'], // 可选的每页显示条数
  showQuickJumper: false, // 是否可以快速跳转到指定页
  showTotal: total => `共 ${total} 条`, // 显示总条数和当前数据范围
  current: 1, // 当前页数
  total: 0, // 总条数
})
function handleResizeColumn(w, col) {
  col.width = w;
}
const rowSelection = {
  onChange: (selectedRowKeys, selectedRows) => {
    console.log(`selectedRowKeys: ${selectedRowKeys}`, 'selectedRows: ', selectedRows);
    selectRow.value = selectedRows
    console.log(selectRow.value.length)
  },
  getCheckboxProps: record => ({
    disabled: record.name === 'Disabled User',
    name: record.name,
  }),
};

const handlePageChange = (page, pageSize) => {
  search(page)
}

const job = reactive({
  jobName:'',
  category: '',
  assemblyName: '',
  typeName: '',
  jobKey: '',
  jobGroup: '',
  cronExpression: '',
  status: 1,
  concurrent: false,
  seconds: 0,
  repeat: -1,
  id: -1,
})
const resetJob = () => {
  job.jobName = ''
  job.category = ''
  job.assemblyName = ''
  job.typeName = ''
  job.jobKey = ''
  job.jobGroup = ''
  job.cronExpression = ''
  job.status = 1
  job.concurrent = false
  job.seconds = 0
  job.repeat = -1
  job.id = -1
}
// 删除计划
const delJob = () => {
  if (selectRow.value.length === 0){
    return;
  }
  let data = selectRow.value.map( item => item.id);
  DelJob(data).then(res => {
    if (res.code === 200){
      message.success("删除成功")
      search(null)
    }else {
      message.error(res.msg)
    }
  })

}
// 暂停或继续计划
const puseJob = (state) => {

  if (selectRow.value.length === 0){
    return;
  }
  selectRow.value.forEach(item => {
    let data = {id: item.id, status:  state}
    StatusJob(data).then(res => {
      if (res.code === 200){
        message.success("操作成功")
        search(null)
      }else {
        message.error(res.msg)
      }
    })
  })

}
const disable = ref(false)
const title = ref('')
// 新增
const addJob = () => {
  open.value = true
  title.value = '新增计划'
  resetJob()
}
// 编辑
const updateJob = () => {
  if (selectRow.value.length === 0){
    return;
  }
  open.value = true
  title.value = '修改计划'

  job.jobName = selectRow.value[0].jobName
  job.category = selectRow.value[0].category
  job.assemblyName = selectRow.value[0].assemblyName
  job.typeName = selectRow.value[0].typeName
  job.jobKey = selectRow.value[0].jobKey
  job.jobGroup = selectRow.value[0].jobGroup
  job.cronExpression = selectRow.value[0].cronExpression
  job.status = selectRow.value[0].status
  job.concurrent = selectRow.value[0].concurrent
  job.seconds = selectRow.value[0].seconds
  job.repeat = selectRow.value[0].repeat
  job.id = selectRow.value[0].id

  disable.value = true

}


const open = ref(false);

const onClose = () => {
  open.value = false;
  disable.value = false;
};

const saveJob = () => {

  let data = toRaw(job)
/*  data.concurrent = job.concurrent === 'false' ? false : true;
  data.status = Number(job.status)*/
  if (!disable.value){
    AddJob(data).then(res => {
      if (res.code === 200){
        message.success("保存成功")
        resetJob()
        onClose()
        search(null)
        disable.value = false
      }
      else {
        message.error(res.msg)
      }
    })
  }else {
    UpdateJob(data).then(res => {
      if (res.code === 200){
        message.success("修改成功")
        resetJob()
        onClose()
        search(null)
        disable.value = false
      }
      else {
        message.error(res.msg)
      }
    })
  }


}
const optionsWithDisabled = [
  { label: '正常', value: 0 },
  { label: '暂停', value: 1 },
];

</script>

<template>
  <a-card>
    <a-flex :gap="50">
      <a-flex gap="middle">
        <label class="from-label">计划名称</label>
        <a-input v-model:value="query.jobName" placeholder="请输入计划名称" />
      </a-flex>

      <a-flex gap="middle">
        <label class="from-label">计划分类</label>
        <a-input v-model:value="query.category" placeholder="请输入计划分类" />
      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">计划状态</label>
        <a-select
            ref="select"
            style="width: 120px"
            v-model:value="query.status"
        >
          <a-select-option :value="0">正常</a-select-option>
          <a-select-option :value="1">暂停</a-select-option>
        </a-select>

      </a-flex>

      <a-flex gap="large">
        <a-button type="primary" @click="search(null)" block> <SearchOutlined />查询</a-button>
        <a-button block @click="reset"> <RedoOutlined />重置</a-button>
      </a-flex>

    </a-flex>


  </a-card>

  <a-card style="margin-top: 10px;">

    <a-button  @click="addJob"   style="margin: 10px;background-color: #2ecc71;color: white;">
      <FormOutlined />
      新增
    </a-button>
    <a-button @click="updateJob"  style="margin: 10px;background-color: #f1c40f;color: white;">
      <FormOutlined />
      编辑
    </a-button>
    <a-popconfirm
        title="是否确定删除？"
        ok-text="Yes"
        cancel-text="No"
        @confirm="delJob"
    >
    <a-button type="primary" danger  style="margin: 10px;">
      <DeleteOutlined />
      删除
    </a-button>
    </a-popconfirm>
    <a-button type="primary"  @click="puseJob(1)" style="margin: 10px;">
      <CaretRightOutlined />
      暂停
    </a-button>
    <a-button type="primary"  @click="puseJob(0)" style="margin: 10px;">
      <CaretRightOutlined />
      继续
    </a-button>
    <a-button type="primary" @click="logTarget"   style="margin: 10px;background-color: #2ecc71;color: white;">
      <SlidersOutlined />
      日志
    </a-button>

    <a-table :data-source="dataSource" :columns="columns"  :scroll="{ x: 1800 }" row-key="id"  size="middle"
             :row-class-name="(_record, index) => (index % 2 === 1 ? 'table-striped' : null)"
             :row-selection="rowSelection"  @change="handlePageChange"
             :pagination="paginationer"   >
      <template #bodyCell="{ column, text, record }">
        <template v-if="column.dataIndex === 'status'">
          <a-tag v-if="record.status === 0" color="#87d068">正常</a-tag>
          <a-tag v-else color="#f50">暂停</a-tag>
        </template>

        <template v-else-if="column.dataIndex === 'typeName'">
          <a-tooltip placement="topLeft" :title="record.typeName" >{{record.typeName}}</a-tooltip>
        </template>

        <template v-else-if="column.dataIndex === 'concurrent'">
          <a-tag v-if="record.concurrent === true" color="#87d068">是</a-tag>
          <a-tag v-else color="#f50">否</a-tag>
        </template>

      </template>

    </a-table>

  </a-card>



  <a-drawer
      title="新增计划"
      placement="right"
      :closable="false"
      :open="open"
      @close="onClose"
      width="500"
  >
    <a-flex  vertical :gap="50">

      <a-flex gap="middle">
        <label class="from-label" >计划名称</label>
        <a-input class="label-right" v-model:value="job.jobName" placeholder="请输入计划名称" />
      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">计划类别</label>
        <a-input class="label-right" v-model:value="job.category" placeholder="请输入计划类别" />
      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">jobKey&nbsp;&nbsp;&nbsp;</label>
        <a-input class="label-right" v-model:value="job.jobKey" placeholder="请输入计划jobKey" :disabled="disable" />
      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">jobGroup</label>
        <a-input class="label-right" v-model:value="job.jobGroup" placeholder="请输入计划jobGroup"  :disabled="disable" />
      </a-flex>

      <a-flex gap="middle">
        <label class="from-label">程序集名</label>
        <a-input class="label-right" v-model:value="job.assemblyName" placeholder="请输入计划程序集名" />
      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">计划内容全类名</label>
        <a-input style="width: 250px;margin-left: 20px;" v-model:value="job.typeName" placeholder="请输入计划类名" />
      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">cron 表达式</label>
        <a-input  style="margin-left: 40px;width: 250px;" v-model:value="job.cronExpression" placeholder="请输入计划cron表达式" />
      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">cron并发执行</label>
        <a-select
            ref="select"
            style="margin-left: 30px;width: 250px;"
            v-model:value="job.concurrent"
            class="label-right"
        >
          <a-select-option :value="true">并发</a-select-option>
          <a-select-option :value="false">串行</a-select-option>
        </a-select>
      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">计划状态</label>
        <a-radio-group   class="label-right" v-model:value="job.status" optionType="button"
                         button-style="solid" >
          <a-radio-button :value="0">正常</a-radio-button>
          <a-radio-button :value="1">暂停</a-radio-button>
        </a-radio-group>
      </a-flex>

      <a-flex vertical>
        <a-button type="primary"  @click="saveJob"   style="margin-top: 30px;">
          保存
        </a-button>
        <a-button    @click="resetJob"   style="margin-top: 20px;">
          重置
        </a-button>
      </a-flex>

    </a-flex>


  </a-drawer>

</template>

<style scoped>
.ant-input {
  width: 200px;
}
.label-right {
  margin-left: 60px;
  width: 250px;
}


</style>