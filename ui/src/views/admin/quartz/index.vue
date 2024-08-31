<script setup>
import {SearchOutlined, RedoOutlined, DeleteOutlined, FormOutlined, CaretRightOutlined, SlidersOutlined} from '@ant-design/icons-vue';
import locale from "ant-design-vue/es/date-picker/locale/zh_CN.js";
import {reactive, ref, onMounted, } from 'vue';
import {message} from "ant-design-vue";
import {FindQuartz} from "@/api/quartz.js";
import {useRouter} from "vue-router";


defineOptions({
  name : 'sysquartz'
})

onMounted(() => {

  search()

})

const search = () => {
  let form = {jobName: '', category: '', status: undefined, pageNum: 1, pageSize: 10  };
  FindQuartz(form).then(res => {
    if (res.code === 200){
      let result = res.data
      dataSource.value = result.rows
      paginationer.total = result.total
      paginationer.current = result.pageNum
      paginationer.pageSize = result.pageSize
    }
  })

}

const reset = () => {

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
  {    title: '操作',      key: 'operation',    dataIndex: 'operation',  align: 'center'  },
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
  console.log(page)
}
</script>

<template>
  <a-card>
    <a-flex :gap="50">
      <a-flex gap="middle">
        <label class="from-label">计划名称</label>
        <a-input v-model:value="path" placeholder="请输入计划名称" />
      </a-flex>

      <a-flex gap="middle">
        <label class="from-label">计划分类</label>
        <a-input v-model:value="operation" placeholder="请输入计划分类" />
      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">计划状态</label>
        <a-select
            ref="select"
            style="width: 120px"
            @change="handleChange"
            v-model:value="disabled"
        >
          <a-select-option value="0">正常</a-select-option>
          <a-select-option value="1">暂停</a-select-option>
        </a-select>

      </a-flex>


      <a-flex gap="large">
        <a-button type="primary" @click="search('')" block> <SearchOutlined />查询</a-button>
        <a-button block @click="reset"> <RedoOutlined />重置</a-button>
      </a-flex>

    </a-flex>


  </a-card>

  <a-card style="margin-top: 10px;">

    <a-button type="primary"   style="margin: 10px;">
      <FormOutlined />
      新增
    </a-button>
    <a-button   style="margin: 10px;background-color: #f1c40f;color: white;">
      <FormOutlined />
      编辑
    </a-button>
    <a-popconfirm
        title="是否确定删除？"
        ok-text="Yes"
        cancel-text="No"
        @confirm="delLog"
    >
    <a-button type="primary" danger  style="margin: 10px;">
      <DeleteOutlined />
      删除
    </a-button>
    </a-popconfirm>
    <a-button type="primary"   style="margin: 10px;">
      <CaretRightOutlined />
      暂停
    </a-button>
    <a-button type="primary"   style="margin: 10px;">
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

        <template v-else-if="column.key === 'operation'">
          <a-button v-if="record.status === 0" type="primary" >暂停</a-button>
          <a-button v-else type="primary" >继续</a-button>
        </template>
      </template>

    </a-table>

  </a-card>
</template>

<style scoped>
.ant-input {
  width: 200px;
}


</style>