<script setup>
import {ref, watch, reactive, toRef, toRaw, onMounted} from "vue";
import dayjs from "dayjs";
import locale from 'ant-design-vue/es/date-picker/locale/zh_CN';
import {message} from "ant-design-vue";
import {UserAddOutlined, SearchOutlined, RedoOutlined } from '@ant-design/icons-vue';
import TreeMenu from "@/components/menu/TreeMenu.vue";
import {newmenus} from "@/router/menu.js";
import {
  AadBindUserRole,
  AddPermissons,
  AddRole,
  DelRole,
  GetPermissonsMenuIdsByRole,
  ListRole,
  PutRole
} from "@/api/role.js";
import {checkAllPropertiesEmpty} from "@/utils/baseUtils.js";
import {ListMenus} from "@/api/menu.js";
import CheckedUser from "@/components/user/CheckedUser.vue";

defineOptions({
  name : 'role'
})

onMounted(() =>{
    getList({})
    getTree()
})

const getList =(data) =>{
    ListRole(data).then(res => {
        if (res.code === 200)
        dataSource.value = res.data
    })
}

const list = () =>{
    let data = {}
    if (roleName.value !== undefined && roleName.value !== '' ){
        data.roleName = roleName.value
    }
     if (key.value !== undefined && key.value !== '' ){
        data.key = key.value
    }

    if (disabled.value !== undefined && disabled.value !== '' ){
        data.status = disabled.value
    }
    if( time.value !== undefined && time.value.length !== 0){

        data.start = dayjs(time.value[0]).format('YYYY-MM-DD')
        data.end = dayjs(time.value[1]).format('YYYY-MM-DD')
    }

    getList(data)

}

const roleName = ref('')
const key = ref('')
const disabled = ref('')
const time = ref([])

const reset = () =>{
  roleName.value = ''
  key.value = ''
  disabled.value = ''
  time.value = []
}

const handleChange = value => {
  console.log(`selected ${value}`);
  disabled.value = value;
};
watch(time,() =>{
  // console.log(dayjs(startedTime[0], 'YYYY-MM-DD'))
  /*console.log(dayjs(time[0]).format('YYYY-MM-DD'))
  console.log(dayjs(time[1]).format('YYYY-MM-DD'))*/
})

const handleChangeStatus = (item) =>{
  console.log(item)
}

const dataSource = ref([

])
const columns= [
  {
    title: '角色名称',
    dataIndex: 'roleName',
    key: 'roleName',
    width: 100,
    align: 'center'
  },
  {
    title: '权限字符',
    dataIndex: 'roleKey',
    key: 'roleKey',
    width: 70,
    align: 'center'
  },
  {
    title: '状态',
    dataIndex: 'disabled',
    key: 'disabled',
    width: 50,
    align: 'center',
    scopedSlots: { customRender: 'showDecimalsOrnot'}
  },
  {
    title: '备注',
    dataIndex: 'remark',
    key: 'remark',
    width: 100,
    align: 'center',
  },
  {
    title: '创建人',
    dataIndex: 'createdBy',
    key: 'createdBy',
    width: 100,
    align: 'center'
  },
  {
    title: '创建时间',
    dataIndex: 'createdTime',
    key: 'createdTime',
    width: 100,
    align: 'center'
  },
  {
    title: '操作',
    key: 'operation',
    dataIndex: 'operation',
    fixed: 'right',
    // align: 'center',
    width: 100,
  },

]

const  paginationer = reactive({
  size: "small",
   simple: true,
  // 分页配置
/*  pageSize: 10, // 每页显示的条数
  showSizeChanger: true, // 是否可以改变每页显示的条数
  pageSizeOptions: ['10', '20', '30', '40'], // 可选的每页显示条数
  showQuickJumper: false, // 是否可以快速跳转到指定页
  showTotal: total => `共 ${total} 条`, // 显示总条数和当前数据范围
  current: 1, // 当前页数
  total: 20, // 总条数
  locale: locale,*/

})


const confirm = (id) => {

    DelRole(id).then(res => {
        if (res.code === 200){
            message.success(res.message);
            getList({})
        }
    })
};
const cancel = e => {
  message.error('Click on No');
};

const open = ref(false);
const showAddRole = ref(false);

const currentRole = ref({});
const showModal = (item) => {

  console.log(item)
  currentRole.value = item
  open.value = true;
}
const showAdd = () => {
  currentRole.value = {}
  showAddRole.value = true;
}

const handleOkAdd = e => {

    let data1 = toRaw(currentRole)
    let data = { roleName: currentRole.value.roleName, key:  currentRole.value.key, status:  currentRole.value.disabled, remark:  currentRole.value.remark }
   /* if (checkAllPropertiesEmpty(data)){
        return  false;
    }*/
    if (data.roleName === undefined||data.roleName === '' ||data.key === undefined || data.key === ''){
        message.warn("请输入角色名称或字符")
        return  false;
    }
    AddRole(data).then(res => {
        if (res.code === 200){
            message.success(res.message);
            showAddRole.value = false;
            getList({})
        }
    })


};
const handleOkPut = e => {

    let data1 = toRef(currentRole)
    PutRole(data1.value).then(res => {
        if (res.code === 200){
            message.success(res.message);
            open.value = false;
            getList({})
        }
    })


};

const layout = {
  labelCol: {
    span: 8,
  },
  wrapperCol: {
    span: 16,
  },
};
const validateMessages = {
  required: '${label} is required!',
};

const onFinish = values => {
  console.log('Success:', values);
};


const permisson = ref(false)
const perFuc = (item) => {
  currentRole.value = item
  permisson.value = true

  GetPermissonsMenuIdsByRole(item.id).then(res => {
    checkedKeys.value = res.data
  })

}
const savePermission = () => {
  let data = {'roleId': currentRole.value.id }
  if (checkStrictly.value){
    data.menuIds = checkedKeys.value.checked
  }else {
    data.menuIds = checkedKeys.value
  }

  AddPermissons(data).then( res => {
    if (res.code === 200){
      message.success("操作成功")
      permisson.value = false
    }
    else {
      message.error(res.msg)

    }
  })

}

const treeData = ref([
  {
    title: '主类目',
    id: 0,
    key: 0,
    children: [],
  },
]);
const selectedKeys = ref([]);
const checkedKeys = ref([ ]);
watch(selectedKeys, () => {
  console.log('selectedKeys', selectedKeys);
});
watch(checkedKeys, () => {
  console.log('checkedKeys', checkedKeys);
});

const getTree = () =>{
  ListMenus({}).then(res => {
    console.log(res.data)
    treeData.value[0].children = res.data;
  })
}

const checkStrictly = ref(true)
const bindUser =  ref(false)
const bindUserFuc = (item) => {
  currentRole.value = item
  bindUser.value = true
}
const selectUserIds = (ids) => {
  userIds.value = ids
}
const userIds = ref([])
const saveBindUser = () => {

  let data = {roleId: currentRole.value.id, 'userIds': userIds.value}
  AadBindUserRole(data).then(res => {
    if (res.code === 200){
      message.success("保存成功");
      currentRole.value = {}
      bindUser.value = false
    }else {
      message.error(res.msg)
    }
  })

}

// 添加父子联动、全选
</script>



<template>
  <a-card>
    <a-flex :gap="50">
      <a-flex gap="middle">
      <label class="from-label">角色名称</label>
      <a-input v-model:value="roleName" placeholder="请输入角色名称" />
      </a-flex>

      <a-flex gap="middle">
      <label class="from-label">权限字符</label>
      <a-input v-model:value="key" placeholder="请输入角色字符" />
      </a-flex>

      <a-flex gap="middle">
      <label class="from-label">状态</label>
        <a-select
            ref="select"
            style="width: 120px"
            @change="handleChange"
            v-model:value="disabled"
        >
          <a-select-option value="0">正常</a-select-option>
          <a-select-option value="1">停用</a-select-option>
        </a-select>

      </a-flex>
      <a-flex gap="middle">
      <label class="from-label">创建时间</label>
        <a-range-picker v-model:value="time" :locale="locale" />
      </a-flex>

      <a-flex gap="large">
        <a-button type="primary" @click="list" block> <SearchOutlined />查询</a-button>
        <a-button block @click="reset"> <RedoOutlined />重置</a-button>
      </a-flex>

    </a-flex>


  </a-card>

   <a-card style="margin-top: 10px;">
     <a-button type="primary" @click="showAdd"  style="margin: 10px;">
       <UserAddOutlined />
       添加角色
     </a-button>
     <a-table :data-source="dataSource" :columns="columns" row-key="id"
            :pagination="paginationer"   >
       <template #bodyCell="{ column, text, record }">
         <template slot="showDecimalsOrnot" v-if="column.dataIndex === 'disabled'">
           <a-switch
                     :checked="record.disabled === 0"
                     checked-children="正常" un-checked-children="停用" />
         </template>

         <template v-else-if="column.key === 'operation'">
           <a-flex>
             <a-button type="text" style="color: #00b96b;" @click="showModal(record)">编辑</a-button>
             <a-popconfirm
                 title="是否删除此角色?"
                 ok-text="确定"
                 cancel-text="取消"
                 @confirm="confirm(record.id)"
                 @cancel="cancel"
             >
               <a-button type="text"  danger>删除</a-button>
             </a-popconfirm>
             <a-button type="text" style="color: #00b96b;" @click="perFuc(record)" >菜单权限</a-button>
             <a-button type="text" style="color: #00b96b;" @click="bindUserFuc(record)">分配用户</a-button>
           </a-flex>
         </template>
       </template>

     </a-table>

  </a-card>

  <a-modal v-model:open="open" title="编辑角色信息" @ok="handleOkPut" cancelText="取消" okText="保存">
    <a-form

        v-bind="layout"
        name="nest-messages"
        :validate-messages="validateMessages"
        @finish="onFinish"
    >
      <a-form-item :name="['roleName']" label="角色名称" :rules="[{ required: true }]" x>
        <a-input v-model:value="currentRole.roleName"  disabled />
      </a-form-item>
      <a-form-item :name="[ 'roleKey']" label="权限字符" :rules="[{ required: true }]">
        <a-input v-model:value="currentRole.roleKey" disabled />
      </a-form-item>
        <a-form-item  label="角色状态" >
            <a-radio-group v-model:value="currentRole.disabled" name="radioGroup">
                <a-radio value="0">正常</a-radio>
                <a-radio value="1">停用</a-radio>
            </a-radio-group>
        </a-form-item>
      <a-form-item :name="['remark']" label="备注">
        <a-textarea v-model:value="currentRole.remark" />
      </a-form-item>
    </a-form>
  </a-modal>


  <a-modal v-model:open="showAddRole" title="新增角色" @ok="handleOkAdd" cancelText="取消" okText="保存">
    <a-form
        :model="currentRole"
        v-bind="layout"
        name="nest-messages"
        :validate-messages="validateMessages"
        @finish="onFinish"
    >
      <a-form-item :name="['roleName']" label="角色名称" :rules="[{ required: true }]">
        <a-input v-model:value="currentRole.roleName" />
      </a-form-item>
      <a-form-item :name="[ 'key']" label="权限字符" :rules="[{ required: true }]">
        <a-input v-model:value="currentRole.key" />
      </a-form-item>
      <a-form-item  label="角色状态" >
        <a-radio-group v-model:value="currentRole.disabled" name="radioGroup">
          <a-radio value="0">正常</a-radio>
          <a-radio value="1">停用</a-radio>
        </a-radio-group>
      </a-form-item>

      <a-form-item :name="['remark']" label="备注">
        <a-textarea v-model:value="currentRole.remark" />
      </a-form-item>
    </a-form>
  </a-modal>

  <a-modal v-model:open="permisson" title="修改权限" :destroyOnClose="true" ok-text="保存" @ok="savePermission"
      cancel-text="取消"
  >

    <a-flex :vertical="true" gap="middle" justify="center" align="center">
      <a-form-item  label="角色名称" >
        <a-input v-model:value="currentRole.roleName" disabled/>
      </a-form-item>

      <a-form-item  label="权限字符" >
        <a-input v-model:value="currentRole.roleKey" disabled />
      </a-form-item>

    </a-flex>


      <a-tree
          v-model:selectedKeys="selectedKeys"
          v-model:checkedKeys="checkedKeys"
          :checkStrictly="checkStrictly"
          checkable
          :height="253"
          :tree-data="treeData"
      >
        <template #title="{ title, id }">
          <span v-if="id === '0-0-1-0'" style="color: #1890ff">{{ title }}</span>
          <template v-else>{{ title }}</template>
        </template>
      </a-tree>


  </a-modal>

  <a-modal v-model:open="bindUser" title="分配用户" :destroyOnClose="true" ok-text="保存" @ok="saveBindUser"
    width="1550px" cancel-text="取消"
  >
    <CheckedUser @userIdsChange="selectUserIds" :roleId="currentRole.id" />
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