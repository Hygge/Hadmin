<script setup>
import {RedoOutlined, } from "@ant-design/icons-vue";
import {reactive, ref, watch, onMounted , toRaw } from "vue";
import {copyObj, resetObj} from "@/utils/baseUtils.js";
import {newmenus} from "@/router/menu.js";
import {message} from "ant-design-vue";
import {ListMenus, AddMenus, ModifyMenus, RemoveMenus} from "@/api/menu.js";

defineOptions({
  name : 'menuList'
})


onMounted(()=>{
  getList()


})
const  getList = (data) => {
  ListMenus(data).then(res => {
    if (res.code === 200){
      tableData.value = res.data
      treeData.value[0].children = res.data
    }
  })
}

const search = reactive({
  title : '',
  status : undefined,
})
const searchFuc = () =>{

  getList(toRaw(search))
}
const serachResetFunc = () =>{
  resetObj(search)
}
const handleChange = value => {
  console.log(`selected ${value}`);
  search.status = value;
  console.log(`selected ${value}`);
}



const columns = [
  {
    title: '菜单名称',
    dataIndex: 'title',
    width: '10%',
    key: 'title',
  },
  {
    title: '图标',
    dataIndex: 'icon',
    key: 'icon',
    width: '9%',

  },
  {
    title: '排序',
    dataIndex: 'order',
    key: 'order',
    width: '5%',
  },
  {
    title: '权限标识符',
    dataIndex: 'pem',
    key: 'pem',
    width: '10%',
  },
  {
    title: '组件路径',
    dataIndex: 'path',
    key: 'path',
    width: '13%',
  },
  {
    title: '状态',
    dataIndex: 'enable',
    key: 'enable',
    width: '7%',
  },
  {
    title: '创建时间',
    dataIndex: 'createdTime',
    key: 'createdTime',
    width: '10%',
  },
  {
    title: '操作',
    dataIndex: 'operation',
    key: 'operation',
    width: '14%',
  },

];
const tableData = ref([]);

const confirm = (id) => {
  RemoveMenus(id).then(res=>{
    if (res.code === 200){
      message.success('删除成功！');
    }
  })
};
const cancel = e => {
  console.log(e);
  message.info('取消操作');
};

const currentMenu = ref({})
const type = ref(1)
const pop = ref(false)
const popTitle = ref('')

const addOrUpdate = (item, str) => {

  if (str === 'add'){
    popTitle.value = '新增菜单'
    value.value = item.key
    type.value = 1
    resetObj(currentMenu.value)
  }
  if (str === 'update'){
    popTitle.value = '修改菜单'
    value.value = item.parentId
    type.value = item.type
    currentMenu.value = item
  }
  console.log(currentMenu)
  pop.value = true;
}

const value = ref('');
const parentId = ref(0);
const treeData = ref([
  {
    title: '主类目',
    id: 0,
    key: '0',
    children: [],
  },
]);
const dir = ref(false)
const menu = ref(false)
const btn = ref(false)
watch(type, () => {
  console.log(type.value)
  if (type.value === 1){
    dir.value = true
    menu.value = false
    btn.value = false
  }
  if (type.value === 2){
    menu.value = true
    dir.value = true
    btn.value = false
  }
  if (type.value === 3){
    btn.value = true
    menu.value = false
    dir.value = false
  }
  currentMenu.value.type = type.value
  console.log(currentMenu.value.icon)
});
watch(value, () => {
  console.log(value.value)
})
const listIcon = ref([])

const saveMenu = () =>{
  let data = {}
  data.parentId = value.value
  data.title = currentMenu.value.title
  data.icon = currentMenu.value.icon || ''
  data.order = currentMenu.value.order || undefined
  data.pem = currentMenu.value.pem
  data.show = currentMenu.value.show  || 1
  data.target = currentMenu.value.target  || 0
  data.enable = currentMenu.value.enable
  data.type = type.value
  data.query = currentMenu.value.query  || ''
  data.path = currentMenu.value.path  || ''
  data.route = currentMenu.value.route || ''
  data.keepAlive = currentMenu.value.keepAlive || ''
  if ( popTitle.value === '新增菜单'){
    AddMenus(data).then(res => {
      if (res.code === 200) {
        message.success("添加成功")
        pop.value = false;
        value.value = undefined
        type.value = undefined
        currentMenu.value = {}
        getList()
      }
    })
  }else {
    data.id = currentMenu.value.id
    ModifyMenus(data).then(res => {
      if (res.code === 200){
        message.success("修改成功");
        pop.value = false;
        value.value = undefined
        type.value = undefined
        currentMenu.value = {}
        getList()
      }
    })

  }

}


</script>

<template>
  <a-card>
    <a-flex :gap="50">
      <a-flex gap="middle">
        <label class="from-label">菜单名称</label>
        <a-input v-model:value="search.title" placeholder="请输入菜单名称" />
      </a-flex>


      <a-flex gap="middle">
        <label class="from-label">状态</label>
        <a-select
            ref="select"
            style="width: 200px"
            @change="handleChange"
            v-model:value="search.status"
        >
          <a-select-option value="1">正常</a-select-option>
          <a-select-option value="0">停用</a-select-option>
        </a-select>

      </a-flex>

      <a-flex gap="large">
        <a-button type="primary" @click="searchFuc" block> <Component :is="$antIcons['SearchOutlined']" />查询</a-button>
        <a-button block @click="serachResetFunc"> <RedoOutlined />重置</a-button>
      </a-flex>

    </a-flex>


  </a-card>

  <a-card style="margin-top: 20px;">
    <a-table :columns="columns" :data-source="tableData"  >
      <template #bodyCell="{ column, text, record }">
        <template slot="showDecimalsOrnot" v-if="column.dataIndex === 'enable'">
          <a-tag :color="record.enable === 1? 'success' : 'default'">{{ record.enable === 1? '正常' : '停用' }}</a-tag>
        </template>

        <template v-else-if="column.key === 'operation'">
          <a-flex>
            <a-button type="text" style="color: #00b96b;" @click="addOrUpdate(record, 'update')">编辑</a-button>

            <a-button type="text" style="color: #00b96b;" @click="addOrUpdate(record, 'add')" >新增</a-button>
            <a-popconfirm
                title="是否删除此数据项?"
                ok-text="确定"
                cancel-text="取消"
                @confirm="confirm(record.id)"
                @cancel="cancel"
            >
              <a-button type="text" danger>删除</a-button>
            </a-popconfirm>
          </a-flex>
        </template>
      </template>
    </a-table>

  </a-card>

  <a-modal v-model:open="pop" :title="popTitle"  width="670px"  >
    <a-form
        style="margin: 20px;"
        layout="inline"
        autocomplete="off"
        name="nest-messages"
    >
      <a-flex :vertical="true" gap="middle" >
      <a-form-item :name="['key']" label="上级菜单" >
        <a-tree-select
            v-model:value="value"
            show-search
            style="width: 80%"
            :dropdown-style="{ maxHeight: '200px', overflow: 'auto' }"
            placeholder="请选择住类目"
            allow-clear
            :tree-data="treeData"
            :field-names="{
                key: 'key',
                label: 'title',
                value: 'key',
                children: 'children'
            }"
        >
        </a-tree-select>
      </a-form-item>
      <a-flex :gap="10">
        <a-form-item  label="菜单名称" :rules="[{ required: true }]">
          <a-input v-model:value="currentMenu.title" />
        </a-form-item>
        <a-form-item :name="['icon']" label="图标" >
          <a-select
              v-model:value="currentMenu.icon"
              size="middle"
              style="width: 200px"
          >
            <a-select-option :value="iconName" :label="iconName"
                             v-for="iconName in Object.keys($antIcons)" :key="iconName">
              <component :is="$antIcons[iconName]" />
              &nbsp;&nbsp;{{iconName}}
            </a-select-option>
          </a-select>
        </a-form-item>
      </a-flex>

        <a-flex :gap="20">
      <a-form-item  label="菜单类型" >
        <a-radio-group v-model:value="type" name="radioGroup">
          <a-radio :value="1" >目录</a-radio>
          <a-radio :value="2" >菜单</a-radio>
          <a-radio :value="3" >按钮</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item :name="['order']" label="显示排序" >
        <a-input-number :min="1" v-model:value="currentMenu.order" />
      </a-form-item>
        </a-flex>

        <a-flex :gap="60" v-if="menu">
          <a-form-item  label="是否外链" >
            <a-radio-group v-model:value="currentMenu.target" name="target">
              <a-radio :value="1" >是</a-radio>
              <a-radio :value="0" >否</a-radio>
            </a-radio-group>
          </a-form-item>
          <a-form-item  label="显示状态" >
            <a-radio-group v-model:value="currentMenu.show" name="show">
              <a-radio :value="1" >显示</a-radio>
              <a-radio :value="0" >隐藏</a-radio>
            </a-radio-group>
          </a-form-item>
        </a-flex>

        <a-form-item  label="菜单状态" >
          <a-radio-group v-model:value="currentMenu.enable" name="enable">
            <a-radio :value="1" >正常</a-radio>
            <a-radio :value="0" >停用</a-radio>
          </a-radio-group>
        </a-form-item>

        <a-flex :gap="10" v-if="menu">
      <a-form-item :name="['route']" label="路由地址" >
        <a-input v-model:value="currentMenu.route" placeholder="请输入路由地址" />
      </a-form-item>
      <a-form-item :name="['path']" label="组件地址" >
        <a-input v-model:value="currentMenu.path" placeholder="请输入组件地址全路径"/>
      </a-form-item>
        </a-flex>

        <a-flex :gap="10" v-if="menu">
          <a-form-item :name="['keepAlive']" label="组件缓存" >
            <a-input v-model:value="currentMenu.keepAlive" placeholder="请输入组件定义name" />
          </a-form-item>
      <a-form-item :name="['query']" label="路由参数" >
        <a-input v-model:value="currentMenu.query" placeholder="请输入路由参数" />
      </a-form-item>
        </a-flex>

        <a-form-item :name="['pem']" label="权限字符" >
          <a-input v-model:value="currentMenu.pem" placeholder="请输入权限字符串" />
        </a-form-item>

      </a-flex>
      <a-flex justify="center" align="center" style="margin-top: 30px; width: 100%;">
      <a-form-item >
        <a-button type="primary" style="width: 200px" @click="saveMenu">保存</a-button>
      </a-form-item>

      </a-flex>
    </a-form>
    <template #footer>

    </template>
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