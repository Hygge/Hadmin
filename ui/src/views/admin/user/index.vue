<script setup>
import {StepForwardOutlined, SearchOutlined, RedoOutlined, UsergroupAddOutlined,} from "@ant-design/icons-vue";
import {reactive, ref, onMounted, toRaw} from "vue";
import locale from "ant-design-vue/es/date-picker/locale/zh_CN.js";
import {resetObj} from "@/utils/baseUtils.js";
import {message} from "ant-design-vue";
import {ListRole} from "@/api/role.js";
import {AddUser, FindUser, RemoveUser, ResetPassword, UpdateUser} from "@/api/user.js";
import dayjs from "dayjs";

defineOptions({
  name : 'user'
})

onMounted(()=>{
    ListRole({}).then(res => {
        // console.log(res)
        if(res.code === 200){
            options.value = res.data.map(item => ({'label':item.roleName, 'value':item.id}) )
            roles.value = res.data
        }
    })
  let data = {current: 1, pageSize: paginationer.pageSize}
  getTables(getCondition(data))

})

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
    width: 10,
    align: 'center'
  },
  {
    title: '用户名称',
    dataIndex: 'userName',
    key: 'userName',
    width: 8,
    align: 'center'
  },
  {
    title: '用户昵称',
    dataIndex: 'nickName',
    key: 'nickName',
    width: 10,
    align: 'center'
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 5,
    align: 'center',
    scopedSlots: { customRender: 'showDecimalsOrnot'}
  },
  {
    title: '角色',
    dataIndex: 'role',
    key: 'role',
    width: 12,
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

  {
    title: '创建人',
    dataIndex: 'createdBy',
    key: 'createdBy',
    width: 10,
    align: 'center'
  },
  {
    title: '创建时间',
    dataIndex: 'createdTime',
    key: 'createdTime',
    width: 10,
    align: 'center'
  },
  {
    title: '操作人',
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 10,
    align: 'center'
  },
  {
    title: '操作时间',
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 10,
    align: 'center'
  },

  {
    title: '操作',
    key: 'operation',
    dataIndex: 'operation',
    fixed: 'right',
    // align: 'center',
    width: 16,
  },

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
const handlePageChange = (page, pageSize) => {
  let data = {current: page.current, pageSize: page.pageSize}
  getTables(getCondition(data))
}
const confirm = (id) => {
  // console.log(id);
  RemoveUser(id).then(res => {
    // console.log(res)
    if (res.code === 200) {
      message.success('删除成功！');
      getTables()
    }
  })
};
const cancel = e => {

};


const showNew = ref(false);

const layout = {
  labelCol: {
    span: 8,
  },
  wrapperCol: {
    span: 16,
  },
};
const formRef = ref();
const currentUser = reactive({
    userName: '',
    nickName: '',
    password: '',
    phoneNumber: '',
    remark: '',
    disabled: undefined,
    roleIds: []
});
const resetForm = () => {
    formRef.value.resetFields();
    roleIds.value = []
    currentUser.remark = ''
    currentUser.disabled = undefined
};
const showUpdate = ref(false)
const showModal = (item) => {

  currentUser.value = item
  showUpdate.value = true;
}
const showAdd = () => {
  currentUser.value = {}
  showNew.value = true;

}
const roles = ref([])
const roleIds = ref([]);
const options = ref([]);
const popupScroll = () => {
  // console.log('popupScroll');
};
const handleOkSave = () => {


}
const onSubmit = () => {
   formRef.value
        .validate()
        .then(() => {
          let user = toRaw(currentUser)
          user.roleIds = roleIds.value
          AddUser(user).then(res => {
            if (res.code === 200){
              message.success("保存成功！")
              resetForm()
              showNew.value = false;
              let data1 = {current: paginationer.current, pageSize: paginationer.pageSize}
              getTables(data1);
            }
          })
        })
        .catch(error => {
            message.warn("请输入必填参数")
        });


};
const handleValidate = (...args) => {
    // console.log(args);
};
const rules = {
    userName: [
        {
            required: true,
          /*  validator: (value)=>{
                if (value === '') {
                    return Promise.reject('Please input the password again');
                } else {
                    return Promise.resolve();
                }
            },*/
            message: '请输入用户名',
        },
    ],
    nickName: [
        {
            required: true,
            message: '请输入用户昵称',
        },
    ],
    password: [
        {
            required: true,
            message: '请输入密码',
        },
    ],
    phoneNumber: [
        {
            required: true,
            message: '请输入手机号码',
        },
    ],

};


const showUser = ref(false)
const modifyUser = ref({})
const modifyShow = (item) => {
  showUser.value = true
  modifyUser.value = item
  // console.log(item)
}
const modifySave =  () => {
  let data= { id: modifyUser.value.id, userName: modifyUser.value.userName, nickName: modifyUser.value.nickName,
    phoneNumber: modifyUser.value.phoneNumber, disabled: modifyUser.value.disabled,remark: modifyUser.value.remark, roleIds: roleIds.value }
  UpdateUser(data.id, data).then(res => {
    if (res.code === 200){
      roleIds.value = []
      modifyUser.value = {}
      showUser.value = false
      message.success("修改成功")
    }
  })

}


const showPasword = ref(false)
const modifyPassword = ref({})
const openUpdatePassword = (item) => {
  showPasword.value = true
  modifyPassword.value = item

}
const resetPassword = () => {
  if (modifyPassword.value.password !== modifyPassword.value.newPassword){
    message.warn("两次密码不一致")
    return ;
  }
  if (modifyPassword.value.password === ''){
    message.warn("新密码不能为空")
    return ;
  }
  let p = {password: modifyPassword.value.password};
  ResetPassword(modifyPassword.value.id, p).then((res) => {
    if (res.code === 200){
      message.success("修改成功")
      showPasword.value = false
      modifyPassword.value = {}
    }
  })

}

</script>

<template>
  <a-card>
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


  </a-card>
  <a-card style="margin-top: 20px;">
    <a-button type="primary" @click="showAdd"  style="margin: 10px;">
      <UsergroupAddOutlined />
      添加用户
    </a-button>
    <a-table :data-source="dataSource" :columns="columns" :scroll="{ x: 1800 }" row-key="id" :pagination="paginationer"  @change="handlePageChange">
      <template #bodyCell="{ column, text, record }">
        <template slot="showDecimalsOrnot" v-if="column.dataIndex === 'status'">
            <a-tag v-if="record.disabled === 0" color="#87d068">正常</a-tag>
            <a-tag v-else color="#f50">停用</a-tag>
        </template>
        <template slot="showDecimalsOrnot" v-else-if="column.dataIndex === 'role'">
          <a-tag style="margin-top: 3px;" v-for="(item,i) in record.roles" color="#2db7f5" :key="i"> {{ item.roleName }}</a-tag>
        </template>

        <template v-else-if="column.key === 'operation'">
          <a-flex>
            <a-button type="text" style="color: #00b96b;" @click="modifyShow(record)">编辑</a-button>
            <a-popconfirm
                title="是否删除此用户?"
                ok-text="确定"
                cancel-text="取消"
                @confirm="confirm(record.id)"
                @cancel="cancel"
            >
              <a-button type="text" danger>删除</a-button>
            </a-popconfirm>
            <a-button type="text" style="color: #00b96b;" @click="openUpdatePassword(record)">重置密码</a-button>
          </a-flex>
        </template>
      </template>

    </a-table>
  </a-card>


  <a-modal v-model:open="showNew" title="新增用户"   :footer="false" >
    <a-form
            ref="formRef"
            :model="currentUser"
            v-bind="layout"
            name="nest-messages"
            @validate="handleValidate"
            @finish="onSubmit"
            :rules="rules"
    >
      <a-form-item  label="用户名称" name="userName">
        <a-input v-model:value="currentUser.userName" />
      </a-form-item>
      <a-form-item  label="用户昵称" name="nickName" >
        <a-input v-model:value="currentUser.nickName" />
      </a-form-item>
      <a-form-item  label="密码" name="password" >
        <a-input v-model:value="currentUser.password" />
      </a-form-item>
      <a-form-item  label="用户状态" >
        <a-radio-group v-model:value="currentUser.disabled" name="radioGroup">
          <a-radio value="0">正常</a-radio>
          <a-radio value="1">停用</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item  label="角色" >
        <a-select
            v-model:value="roleIds"
            :options="options"
            mode="multiple"
            size="middle"
            placeholder="请选择权限角色"
            style="width: 200px"
            @popupScroll="popupScroll"
        >
        </a-select>
      </a-form-item>

      <a-form-item label="电话号码"  name="phoneNumber">
        <a-input v-model:value="currentUser.phoneNumber" />
      </a-form-item>
      <a-form-item  label="备注" >
        <a-textarea v-model:value="currentUser.remark" />
      </a-form-item>
        <a-form-item :wrapper-col="{ span: 14, offset: 6 }" style="text-align: center;">
            <a-button style="width: 200px;" type="primary" html-type="submit">保存</a-button>

        </a-form-item>
    </a-form>
  </a-modal>
  <a-modal v-model:open="showUser" title="修改用户"   :footer="false" >
    <a-form
            v-bind="layout"
            name="nest-messages"

    >
      <a-form-item  label="用户名称" name="userName">
        <a-input v-model:value="modifyUser.userName" disabled />
      </a-form-item>
      <a-form-item  label="用户昵称" name="nickName" >
        <a-input v-model:value="modifyUser.nickName" />
      </a-form-item>
      <a-form-item  label="用户状态" >
        <a-radio-group v-model:value="modifyUser.disabled" name="radioGroup">
          <a-radio value="0">正常</a-radio>
          <a-radio value="1">停用</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item  label="角色" >
        <a-select
            v-model:value="roleIds"
            :options="options"
            mode="multiple"
            size="middle"
            placeholder="请选择权限角色"
            style="width: 200px"
            @popupScroll="popupScroll"
        >
        </a-select>
      </a-form-item>

      <a-form-item label="电话号码"  name="phoneNumber">
        <a-input v-model:value="modifyUser.phoneNumber" />
      </a-form-item>
      <a-form-item  label="备注" >
        <a-textarea v-model:value="modifyUser.remark" />
      </a-form-item>
        <a-form-item :wrapper-col="{ span: 14, offset: 6 }" style="text-align: center;">
            <a-button style="width: 200px;" type="primary" @click="modifySave">保存</a-button>
        </a-form-item>
    </a-form>
  </a-modal>

  <a-modal v-model:open="showPasword" title="重置密码"  @ok="resetPassword"  ok-text="确定" cancel-text="取消" >
    <a-form   v-bind="layout"  name="nest-messages" >
      <a-form-item  label="用户名称" name="userName">
        <a-input v-model:value="modifyPassword.userName" />
      </a-form-item>
      <a-form-item  label="新密码" name="password"  >
        <a-input v-model:value="modifyPassword.password" type="password" placeholder="请输入新密码" />
      </a-form-item>
      <a-form-item  label="新密码" name="newPassword" >
        <a-input v-model:value="modifyPassword.newPassword" type="password" placeholder="请输入新密码"  />
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
  line-height: 32px;
}


</style>