<script setup>
import {SearchOutlined, RedoOutlined, DeleteOutlined} from '@ant-design/icons-vue';
import locale from "ant-design-vue/es/date-picker/locale/zh_CN.js";
import {reactive, ref, onMounted, } from 'vue';
import {DelLog, ListLog} from "@/api/sysLog.js";
import {message} from "ant-design-vue";
import dayjs from "dayjs";

defineOptions({
  name : 'syslog'
})

onMounted(() => {

  search(null);

})

const path = ref('')
const operation = ref('')
const operatorName = ref('')
const operateTime = ref([])

const reset = () => {
  operateTime.value = []
  operatorName.value = ''
  operation.value = ''
  path.value = ''
}
const search = (page) =>{
  let query = {path:path.value, operation: operation.value, operatorName: operatorName.value,
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
  ListLog(query).then(res=>{
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
  DelLog(ids).then(res => {
    if (res.code === 200){
      message.success("删除成功");
      search({current:1, pageSize:paginationer.pageSize})
      return;
    }
    message.error("删除失败");
  })

}
const clearLog = ()=>{

}
const selectRow = ref([])
const dataSource = ref([ ])
const columns= [
  {    title: '日志编号',    dataIndex: 'id',    key: 'id',    width: '10%',    align: 'center'  },
  {    title: '操作名称',    dataIndex: 'operation',    key: 'operation',    align: 'center'  },
  {    title: '操作地址',    dataIndex: 'path',    key: 'path',    align: 'center'  },
  {    title: '请求参数',    dataIndex: 'requestParam',    key: 'requestParam',    align: 'center',  ellipsis: true, },
  {    title: '响应参数',    dataIndex: 'responseParam',    key: 'responseParam',    align: 'center',  ellipsis: true,  },
  {    title: '操作人',    dataIndex: 'operatorName',    key: 'operatorName',    align: 'center'  },
  {    title: '操作状态',    dataIndex: 'executeStatus',    key: 'executeStatus',    align: 'center'  },
  {    title: '执行时间',    dataIndex: 'executeTime',    key: 'executeTime',    align: 'center'  },
  {    title: '异常原因',    dataIndex: 'reason',    key: 'reason',    align: 'center',  ellipsis: true,   },
  {    title: '创建时间',    dataIndex: 'operateTime',    key: 'operateTime',    align: 'center'  },
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
        <label class="from-label">操作路径</label>
        <a-input v-model:value="path" placeholder="请输入操作路径" />
      </a-flex>

      <a-flex gap="middle">
        <label class="from-label">操作名称</label>
        <a-input v-model:value="operation" placeholder="请输入操作名称" />
      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">操作人</label>
        <a-input v-model:value="operatorName" placeholder="请输入操作人" />
      </a-flex>

      <a-flex gap="middle">
        <label class="from-label">操作时间</label>
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

    <a-button type="primary" danger @click="clearLog"  style="margin: 10px;">
      <DeleteOutlined />
      清空日志
    </a-button>

    <a-table :data-source="dataSource" :columns="columns" row-key="id"   size="middle" bordered
             :row-class-name="(_record, index) => (index % 2 === 1 ? 'table-striped' : null)"
             :row-selection="rowSelection"  @change="handlePageChange"
             :pagination="paginationer"  @resizeColumn="handleResizeColumn" >
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

</style>