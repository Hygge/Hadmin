<script setup>
import {SearchOutlined, RedoOutlined, DeleteOutlined} from '@ant-design/icons-vue';
import locale from "ant-design-vue/es/date-picker/locale/zh_CN.js";
import {reactive, ref, onMounted, } from 'vue';
import {DelOtherSysLog, GetListLog, } from "@/api/othersys.js";
import {message} from "ant-design-vue";
import dayjs from "dayjs";

defineOptions({
  name : 'othersyslog'
})

onMounted(() => {

  search(null);

})

const path = ref('')
const operation = ref('')
const executeStatus = ref(undefined)
const sysName = ref('')
const operateTime = ref([])

const reset = () => {
  operateTime.value = []
  executeStatus.value = undefined
  operation.value = ''
  sysName.value = ''
  path.value = ''
}
const search = (page) =>{
  let query = {path:path.value, operation: operation.value, sysName: sysName.value,executeStatus: executeStatus.value,
    pageNum: paginationer.current, pageSize: paginationer.pageSize }
  if (page){
    query.pageNum = page.current;
    query.pageSize = page.pageSize;
  }
  if( operateTime.value.length !== 0){

    query.startTime = dayjs(operateTime.value[0]).format('YYYY-MM-DD')
    query.endTime = dayjs(operateTime.value[1]).format('YYYY-MM-DD')
  }
  list(query)
}

const list = (query) => {
  GetListLog(query).then(res=>{
    if (res.code === 200){
      let result = res.data
      dataSource.value = result.rows
      paginationer.total = result.total
      paginationer.current = result.pageNum
      paginationer.pageSize = result.pageSize
    }
  })

}

const delLog = ()=>{
  let ids = selectRow.value.map( item => item.id);
  DelOtherSysLog(ids).then(res => {
    if (res.code === 200){
      message.success("删除成功");
      search({current:1, pageSize:paginationer.pageSize})
      return;
    }
    message.error(res.message);
  })

}
const clearLog = ()=>{

}
const selectRow = ref([])
const dataSource = ref([ ])
const columns= [
  {    title: '日志编号',    dataIndex: 'id',    key: 'id',     align: 'center', width:140,  },
  {    title: '接口名称',    dataIndex: 'operation',    key: 'operation',    align: 'center', width:140,  },
  {    title: '请求地址',    dataIndex: 'path',    key: 'path',    align: 'center', width:140,  },
  {    title: '请求参数',    dataIndex: 'requestParam',    key: 'requestParam',    align: 'center',  ellipsis: true, width:200,},
  {    title: '响应参数',    dataIndex: 'responseParam',    key: 'responseParam',    align: 'center',  ellipsis: true, width:200,  },
  {    title: '请求系统',    dataIndex: 'sysName',    key: 'sysName',    align: 'center', width:140,   },
  {    title: '处理状态',    dataIndex: 'executeStatus',    key: 'executeStatus',    align: 'center', width:100,   },
  {    title: '执行时间',    dataIndex: 'executeTime',    key: 'executeTime',    align: 'center', width:100,   },
  {    title: '异常原因',    dataIndex: 'reason',    key: 'reason',    align: 'center',  ellipsis: true, width:200,   },
  {    title: '创建时间',    dataIndex: 'operateTime',    key: 'operateTime',    align: 'center', width:140,   },
  {    title: '目标系统',    dataIndex: 'sysTargetName',    key: 'sysTargetName',    align: 'center', width:140,   },
  {    title: '请求ip',    dataIndex: 'ip',    key: 'ip',    align: 'center', width:140,   },
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
</script>

<template>
  <a-card>
    <a-flex :gap="50">
      <a-flex gap="middle">
        <label class="from-label">请求路径</label>
        <a-input v-model:value="path" placeholder="请输入请求路径" />
      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">系统名称</label>
        <a-input v-model:value="sysName" placeholder="请输入请求系统名称" />
      </a-flex>

      <a-flex gap="middle">
        <label class="from-label">状态</label>
        <a-select
            ref="select"
            style="width: 120px"
            v-model:value="executeStatus"
        >
          <a-select-option :value="true">成功</a-select-option>
          <a-select-option :value="false">失败</a-select-option>
        </a-select>
      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">请求时间</label>
        <a-range-picker v-model:value="operateTime" :locale="locale" />
      </a-flex>

      <a-flex gap="large">
        <a-button type="primary" @click="search('')" block> <SearchOutlined />查询</a-button>
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
        删除日志
      </a-button>
    </a-popconfirm>

    <a-table :data-source="dataSource" :columns="columns" row-key="id"   size="middle" bordered
             :row-class-name="(_record, index) => (index % 2 === 1 ? 'table-striped' : null)"
             :row-selection="rowSelection"  @change="handlePageChange"
             :pagination="paginationer" :scroll="{ x: 2200 }" @resize="handleResizeColumn" >
      <template #bodyCell="{ column, text, record }">
        <template  v-if="column.dataIndex === 'executeStatus'">
          <a-tag v-if="record.executeStatus" color="#87d068">成功</a-tag>
          <a-tag v-else color="#f50">失败</a-tag>
        </template>
        <template v-else-if="column.dataIndex === 'executeTime'">
          {{record.executeTime}}毫秒
        </template>
        <template v-else-if="column.dataIndex === 'requestParam'">
          <a-tooltip placement="topLeft" :title="record.requestParam" >{{record.requestParam}}</a-tooltip>
        </template>
        <template v-else-if="column.dataIndex === 'responseParam'">
          <a-tooltip placement="topLeft" :title="record.responseParam" >{{record.responseParam}}</a-tooltip>
        </template>
        <template v-else-if="column.dataIndex === 'reason'">
          <a-tooltip placement="topLeft" :title="record.reason" >{{record.reason}}</a-tooltip>
        </template>

      </template>

    </a-table>

  </a-card>

</template>

<style scoped>
.ant-input {
  width: 200px;
}
.from-label{
  font-weight: bold;
}
</style>