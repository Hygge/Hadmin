<script setup>
import { SearchOutlined, RedoOutlined,} from "@ant-design/icons-vue";
import {reactive, ref, onMounted, computed, defineProps,  } from "vue";
import locale from "ant-design-vue/es/date-picker/locale/zh_CN.js";
import {getRoleBindUser} from "@/api/role.js";
import {FindUser,} from "@/api/user.js";
import dayjs from "dayjs";



onMounted(()=>{
  let data = {current: 1, pageSize: paginationer.pageSize}
  getTables(getCondition(data))

  getRoleBindUser(props.roleId).then( res => {
    if (res.code === 200){
      state.selectedRowKeys = res.data
    }
  })

})

const props = defineProps({
  roleId: Number
});

const getTables = (data) => {
  FindUser(data).then(res => {
    if (res.code === 200){
      let result = res.data
      dataSource.value = result.rows
      paginationer.total = result.total
      paginationer.current = result.pageNum
      paginationer.pageSize = result.pageSize
    }
  })
}

const search = reactive({
  userName : '',
  phoneNumber:'',
  status : undefined,
  startTime: '',
  endTime:'',
  time: [],
})
const searchFuc = () =>{
  let data = {current: paginationer.current, pageSize: paginationer.pageSize}
  getTables(getCondition(data))
}
const getCondition = (data) =>{
  if(search.status){
    data.status = search.status
  }
  if (search.userName !== '')
    data.userName = search.userName
  if (search.phoneNumber !== '')
    data.phone = search.phoneNumber
  if (search.userName !== '')
    data.userName = search.userName
  if( search.time !== undefined && search.time.length !== 0){

    data.startTime = dayjs(search.time[0]).format('YYYY-MM-DD')
    data.endTime = dayjs(search.time[1]).format('YYYY-MM-DD')
  }
  return data
}
const serachResetFunc = () =>{
  search.userName = ''
  search.phoneNumber = ''
  search.status = undefined
  search.startTime = ''
  search.endTime = ''
  search.time = []
}

const dataSource = ref([])
const columns= [
  {
    title: '用户编号',
    dataIndex: 'id',
    key: 'id',
    width: 7,
    align: 'center'
  },
  {
    title: '用户名称',
    dataIndex: 'userName',
    key: 'userName',
    width: 7,
    align: 'center'
  },
  {
    title: '用户昵称',
    dataIndex: 'nickName',
    key: 'nickName',
    width: 7,
    align: 'center'
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 7,
    align: 'center',
    scopedSlots: { customRender: 'showDecimalsOrnot'}
  },
  {
    title: '角色',
    dataIndex: 'role',
    key: 'role',
    width: 7,
    align: 'center'
  },
  {
    title: '电话号码',
    dataIndex: 'phoneNumber',
    key: 'phoneNumber',
    width: 7,
    align: 'center',
  },
  {
    title: '备注',
    dataIndex: 'remark',
    key: 'remark',
    width: 10,
    align: 'center',
  },
]

const options = ref([]);
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
const handlePageChange = (page, pageSize) => {
  let data = {current: page.current, pageSize: page.pageSize}
  getTables(getCondition(data))
}




const state = reactive({
  selectedRowKeys: [],
});

const emit = defineEmits(['userIdsChange'])

const onSelectChange = selectedRowKeys => {
  console.log('selectedRowKeys changed: ', selectedRowKeys);
  state.selectedRowKeys = selectedRowKeys;
  emit("userIdsChange", selectedRowKeys);
};

</script>

<template>

  <a-flex vertical :gap="20" style="width: 1500px;">
    <a-flex :gap="50">
      <a-flex gap="middle">
        <label class="from-label">用户名称</label>
        <a-input v-model:value="search.userName" placeholder="请输入用户名称" />
      </a-flex>

      <a-flex gap="middle">
        <label class="from-label">手机号码</label>
        <a-input v-model:value="search.phoneNumber" placeholder="请输入手机号码" />
      </a-flex>

      <a-flex gap="middle">
        <label class="from-label">状态</label>
        <a-select
            ref="select"
            style="width: 180px"
            v-model:value="search.status"
        >
          <a-select-option value="0">正常</a-select-option>
          <a-select-option value="1">停用</a-select-option>
        </a-select>

      </a-flex>
      <a-flex gap="middle">
        <label class="from-label">创建时间</label>
        <a-range-picker v-model:value="search.time" :locale="locale" />
      </a-flex>

      <a-flex gap="large">
        <a-button type="primary" @click="searchFuc" block> <SearchOutlined />查询</a-button>
        <a-button block @click="serachResetFunc"> <RedoOutlined />重置</a-button>
      </a-flex>

    </a-flex>
    <a-table :data-source="dataSource" :columns="columns" :scroll="{ x: 1800 }" row-key="id"
             :row-selection="{ selectedRowKeys: state.selectedRowKeys, onChange: onSelectChange }" :pagination="paginationer"  @change="handlePageChange">
      <template #bodyCell="{ column, text, record }">
        <template slot="showDecimalsOrnot" v-if="column.dataIndex === 'status'">
          <a-tag v-if="record.disabled === 0" color="#87d068">正常</a-tag>
          <a-tag v-else color="#f50">停用</a-tag>
        </template>
        <template slot="showDecimalsOrnot" v-else-if="column.dataIndex === 'role'">
          <a-tag style="margin-top: 3px;" v-for="(item,i) in record.roles" color="#2db7f5" :key="i"> {{ item.roleName }}</a-tag>
        </template>

      </template>

    </a-table>
  </a-flex>



</template>

<style scoped>
.ant-input {
  width: 150px;
}
.from-label{
  font-weight: bold;
  line-height: 32px;
}


</style>