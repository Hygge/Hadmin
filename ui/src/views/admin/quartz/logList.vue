<script setup>

import {
  DeleteOutlined,
  RedoOutlined,
  SearchOutlined
} from "@ant-design/icons-vue";
import {reactive, ref, onMounted} from "vue";
import {ClearLog, DelLog, GetListLog} from "@/api/quartz.js";
import {useRouter} from "vue-router";
import {message} from "ant-design-vue";

defineOptions({
  name : 'quartzLog'
})

onMounted(() => {
  search(null);
})

const query = reactive({
  jobName:'',
  category: '',
  status: undefined,
})
const reset = ()=>{
  query.jobName = '';
  query.category = '';
  query.status = undefined;
}

const search = (page) => {
  console.log(query.status)
  let form = {jobName: query.jobName, category: query.category, status: query.status, pageNum: paginationer.current,
    pageSize: paginationer.pageSize  };
  if (page !== null){
    form.pageSize = page.pageSize;
    form.pageNum = page.current;
  }


  GetListLog(form).then(res => {
    if (res.code === 200){
      let result = res.data
      dataSource.value = result.rows
      paginationer.total = result.total
      paginationer.current = result.pageNum
      paginationer.pageSize = result.pageSize
    }
  })

}



const selectRow = ref([])
const dataSource = ref([ ])
const columns= [
  {    title: '日志编号',    dataIndex: 'id',    key: 'id',    width: '10%',    align: 'center'  },
  {    title: '计划名称',    dataIndex: 'jobName',    key: 'jobName',    align: 'center'  },
  {    title: '计划分类',    dataIndex: 'category',    key: 'category',    align: 'center'  },
  {    title: '执行全类名',    dataIndex: 'typeName',    key: 'typeName',    align: 'center',  ellipsis: true,  },
  {    title: '执行状态',    dataIndex: 'status',    key: 'status',    align: 'center'  },
  {    title: '日志信息',    dataIndex: 'jobMessage',    key: 'jobMessage',    align: 'center',  ellipsis: true,  },
  {    title: '异常信息',    dataIndex: 'exceptionInfo',    key: 'exceptionInfo',    align: 'center',  ellipsis: true,  },
  {    title: '执行时间',    dataIndex: 'startTime',    key: 'startTime',    align: 'center',  },
  {    title: '结束时间',    dataIndex: 'stopTime',    key: 'stopTime',    align: 'center',  },
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
const delLog = () => {
  if (selectRow.value.length === 0){
    return;
  }
  let data = selectRow.value.map( item => item.id);
  DelLog(data).then(res => {
    if (res.code === 200){
      message.success("删除成功")
      search(null)

    }else {
      message.error(res.msg)
    }
  })
}
const clearLog = () => {

  ClearLog().then(res => {
    if (res.code === 200){
      message.success("清空成功")
      search(null)

    }else {
      message.error(res.msg)
    }
  })
}


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
        <label class="from-label">执行状态</label>
        <a-select
            ref="select"
            style="width: 120px"
            v-model:value="query.status"
        >
          <a-select-option :value="true">成功</a-select-option>
          <a-select-option :value="false">失败</a-select-option>
        </a-select>

      </a-flex>

      <a-flex gap="large">
        <a-button type="primary" @click="search(null)" block> <SearchOutlined />查询</a-button>
        <a-button block @click="reset"> <RedoOutlined />重置</a-button>
      </a-flex>

    </a-flex>


  </a-card>

  <a-card style="margin-top: 10px;">

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
    <a-button type="primary" @click="clearLog"   style="margin: 10px;background-color: #2ecc71;color: white;">
      <DeleteOutlined />
      清空日志
    </a-button>

    <a-table :data-source="dataSource" :columns="columns"  :scroll="{ x: 1800 }" row-key="id"  size="middle"
             :row-class-name="(_record, index) => (index % 2 === 1 ? 'table-striped' : null)"
             :row-selection="rowSelection"  @change="handlePageChange"
             :pagination="paginationer"   >
      <template #bodyCell="{ column, text, record }">
        <template v-if="column.dataIndex === 'status'">
          <a-tag v-if="record.status === true" color="#87d068">成功</a-tag>
          <a-tag v-else color="#f50">失败</a-tag>
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

</template>

<style scoped>
.ant-input {
  width: 200px;
}

</style>