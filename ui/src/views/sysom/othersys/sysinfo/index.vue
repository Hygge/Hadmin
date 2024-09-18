<script setup>
import {reactive, ref, onMounted, toRaw,  } from 'vue';
import {message} from "ant-design-vue";
import {  DeleteOutlined,  FormOutlined,  RedoOutlined,  SearchOutlined,} from "@ant-design/icons-vue";
import {AddOtherSys, DelOtherSys, GetList, OtherSysBuildAppkey, UpdateOtherSys} from "@/api/othersys.js";
import locale from "ant-design-vue/es/date-picker/locale/zh_CN";
import dayjs from "dayjs";

defineOptions({
  name : 'otherSysInfo'
})


onMounted(() => {

  search(null)

})

const query = reactive({
  name:'',
  time: [],
})
const reset = ()=>{
  query.name = '';
  query.time = [];
}

const search = (page) => {
  let form = {sysName: query.name, pageNum: paginationer.current,  pageSize: paginationer.pageSize  };
  if (page !== null){
    form.pageSize = page.pageSize;
    form.pageNum = page.current;
  }
  if( query.time !== undefined && query.time.length !== 0){
    form.startTime = dayjs(query.time[0]).format('YYYY-MM-DD')
    form.endTime = dayjs(query.time[1]).format('YYYY-MM-DD')
  }
  GetList(form).then(res => {
    if (res.code === 200){
      let result = res.data
      dataSource.value = result.rows
      paginationer.total = result.total
      paginationer.current = result.pageNum
      paginationer.pageSize = result.pageSize
    }else {
      message.error(res.msg)
    }
  })

}

const selectRow = ref([])
const dataSource = ref([ ])
const columns= [
  {    title: 'id',    dataIndex: 'id',    key: 'id',   align: 'center' ,   width: 150, },
  {    title: '第三方系统名称',    dataIndex: 'name',    key: 'name',    align: 'left',   width: 200,  },
  {    title: '状态',    dataIndex: 'state',    key: 'state',    align: 'center',   width: 100,  },
  {    title: '密钥',    dataIndex: 'appKey',    key: 'appKey',    align: 'center',  ellipsis: true, width: 250, },
  {    title: '创建时间',    dataIndex: 'createdTime',    key: 'createdTime',    align: 'center',   width: 200,   },
  {    title: '操作',    dataIndex: 'operation',    key: 'operation',   align:'center',  fixed: 'right',   width: 160, },
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
    selectRow.value = selectedRows
  },
  getCheckboxProps: record => ({
    disabled: record.name === 'Disabled User',
    name: record.name,
  }),
};

const handlePageChange = (page, pageSize) => {
  search(page)
}

const sysInfo = reactive({
  name:'',
  state: undefined || 0,
  createdTime: undefined,
  appkey: '',
  id: -1,
})

const del = ()=>{
  let ids = selectRow.value.map( item => item.id);
  DelOtherSys(ids).then(res => {

    if (res.code === 200){
      search(null)
      message.success("删除成功")
    }else {
      message.error(res.message)
    }
  })

}

const buidAppkey = (id) =>{
  if (id === undefined){
    return;
  }
  let data = {id:id}
  OtherSysBuildAppkey(data).then(res =>{
    if (res.code === 200){
      search(null)
      message.success("密钥生成成功")
    }else {
      message.error(res.message)
    }
  })
}

const addDlog = ref(false);
const updateDlog = ref(false);

const showAdd = () =>{
  addDlog.value = true;
}
const showUpdate = () =>{
  updateDlog.value = true;
  let sys = selectRow.value[0];
  sysInfo.id = sys.id;
  sysInfo.name = sys.name;
  sysInfo.appkey = sys.appkey;
  sysInfo.state = sys.state;
  sysInfo.createdTime = sys.createdTime;
}

const save = (str) => {
  let data = toRaw(sysInfo);
  if(str === 'add'){
    AddOtherSys(data).then(res => {
      if (res.code === 200){
        addDlog.value = false;
        message.success("保存成功");
        search(null)
      }else {
        message.error(res.message);
      }

    })

  }else {
    UpdateOtherSys(data).then(res => {
      if (res.code === 200){
        addDlog.value = false;
        message.success("保存成功");
        updateDlog.value = false;
        search(null)
      }else {
        message.error(res.message);
      }
    })

  }
}
const cancel = () =>{
  sysInfo.name = '';
  sysInfo.appkey = '';
  sysInfo.createdTime = '';
  sysInfo.state = 0;
}

const layout = {
  labelCol: {
    span: 8,
  },
  wrapperCol: {
    span: 16,
  },
};
</script>

<template>
  <a-card>
    <a-flex :gap="50">
      <a-flex gap="middle">
        <label class="from-label">系统名称</label>
        <a-input v-model:value="query.name" placeholder="请输入第三方系统名称" />
      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">创建时间</label>
        <a-range-picker v-model:value="query.time" :locale="locale" />
      </a-flex>

      <a-flex gap="large">
        <a-button type="primary" @click="search(null)" block> <SearchOutlined />查询</a-button>
        <a-button block @click="reset"> <RedoOutlined />重置</a-button>
      </a-flex>

    </a-flex>


  </a-card>

  <a-card style="margin-top: 10px;">

    <a-button  @click="showAdd"   style="margin: 10px;background-color: #2ecc71;color: white;">
      <FormOutlined />
      新增
    </a-button>
    <a-button @click="showUpdate"  style="margin: 10px;background-color: #f1c40f;color: white;">
      <FormOutlined />
      编辑
    </a-button>
    <a-popconfirm
        title="是否确定删除？"
        ok-text="Yes"
        cancel-text="No"
        @confirm="del"
    >
      <a-button type="primary" danger  style="margin: 10px;">
        <DeleteOutlined />
        删除
      </a-button>
    </a-popconfirm>

    <a-table :data-source="dataSource" :columns="columns"  :scroll="{ x: 1500 }" row-key="id"  size="middle"
             :row-class-name="(_record, index) => (index % 2 === 1 ? 'table-striped' : null)"
             :row-selection="rowSelection"  @change="handlePageChange"
             :pagination="paginationer" :locale="locale"  :expand-column-width="100" >
      <template #bodyCell="{ column, text, record }">
        <template v-if="column.dataIndex === 'state'">
          <a-tag v-if="record.state === 0" color="#87d068">正常</a-tag>
          <a-tag v-else color="#f50">暂停</a-tag>
        </template>

        <template v-else-if="column.dataIndex === 'appKey'">
          <a-tooltip placement="topLeft" :title="record.appKey" >{{record.appKey}}</a-tooltip>
        </template>
        <template v-else-if="column.dataIndex === 'operation'">
          <a-button style="width: 100px;" type="primary" @click="buidAppkey(record.id)" >刷新密钥 </a-button>
        </template>
      </template>

    </a-table>

  </a-card>

  <a-modal v-model:open="addDlog" title="新增系统"  @cancel="cancel" @ok="save('add')" ok-text="保存" cancel-text="取消"  >
    <a-form
        ref="formRef"
        v-bind="layout"
        name="nest-messages"
    >
      <a-form-item  label="第三方系统名称" name="name">
        <a-input v-model:value="sysInfo.name" />
      </a-form-item>
      <a-form-item  label="状态" >
        <a-radio-group v-model:value="sysInfo.state" name="radioGroup">
          <a-radio :value="0">正常</a-radio>
          <a-radio :value="1">停用</a-radio>
        </a-radio-group>
      </a-form-item>

    </a-form>
  </a-modal>
  <a-modal v-model:open="updateDlog" title="修改系统信息"  @cancel="cancel" @ok="save('')" ok-text="保存" cancel-text="取消"  >
    <a-form
        ref="formRef"
        v-bind="layout"
        name="nest-messages"
    >
      <a-form-item  label="第三方系统名称" name="name">
        <a-input v-model:value="sysInfo.name" />
      </a-form-item>
      <a-form-item  label="状态" >
        <a-radio-group v-model:value="sysInfo.state" name="radioGroup">
          <a-radio :value="0">正常</a-radio>
          <a-radio :value="1">停用</a-radio>
        </a-radio-group>
      </a-form-item>

    </a-form>
  </a-modal>
</template>

<style scoped>
.ant-input {
  width: 200px;
}
.from-label{
  font-weight: bold;
}
</style>