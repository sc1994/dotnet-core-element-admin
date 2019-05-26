<template>
  <div class="app-container">
    <el-table :data="tableData" style="width: 100%">
      <el-table-column type="expand">
        <template slot-scope="props">
            <el-table :data="tableData" style="width: 100%" border>
              <el-table-column label="Id" prop="id"> </el-table-column>
              <el-table-column label="方法名" prop="name"> </el-table-column>
              <el-table-column label="入参" prop="desc"> </el-table-column>
              <el-table-column label="返回值" prop="desc"> </el-table-column>
              <el-table-column label="耗时" prop="desc"> </el-table-column>
              <el-table-column label="异常堆栈" prop="desc"> </el-table-column>
            </el-table>
        </template>
      </el-table-column>
      <el-table-column label="TracerId" prop="id"> </el-table-column>
      <el-table-column label="方法名" prop="name"> </el-table-column>
      <el-table-column label="入参" prop="desc"> 
        <template slot-scope="props">
          <el-tooltip placement="right"  effect="light">
            <div slot="content">
              <div v-html="html"></div>
            </div>
            <el-button type="text">Top center.....</el-button>
          </el-tooltip>
        </template>
      </el-table-column>
      <el-table-column label="返回值" prop="desc"> </el-table-column>
      <el-table-column label="耗时" prop="desc"> </el-table-column>
      <el-table-column label="异常堆栈" prop="desc"> </el-table-column>
    </el-table>
    <br/>
    <div style="text-align: end;">
      <el-pagination background layout="prev, pager, next" :total="1000"/>
    </div>
    </el-pagination>
  </div>
</template>

<script>
export default {
  data() {
    return {
      html:"123<br/>&nbsp;&nbsp;&nbsp;123<br/>&nbsp;&nbsp;&nbsp;123<br/>&nbsp;&nbsp;&nbsp;123<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;123<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;123<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>123<br/>",
      tableData: [
        {
          id: "12987122",
          name: "好滋好味鸡蛋仔",
          category: "江浙小吃、小吃零食",
          desc: "荷兰优质淡奶，奶香浓而不腻",
          address: "上海市普陀区真北路",
          shop: "王小虎夫妻店",
          shopId: "10333"
        },
        {
          id: "12987123",
          name: "好滋好味鸡蛋仔",
          category: "江浙小吃、小吃零食",
          desc: "荷兰优质淡奶，奶香浓而不腻",
          address: "上海市普陀区真北路",
          shop: "王小虎夫妻店",
          shopId: "10333"
        },
        {
          id: "12987125",
          name: "好滋好味鸡蛋仔",
          category: "江浙小吃、小吃零食",
          desc: "荷兰优质淡奶，奶香浓而不腻",
          address: "上海市普陀区真北路",
          shop: "王小虎夫妻店",
          shopId: "10333"
        },
        {
          id: "12987126",
          name: "好滋好味鸡蛋仔",
          category: "江浙小吃、小吃零食",
          desc: "荷兰优质淡奶，奶香浓而不腻",
          address: "上海市普陀区真北路",
          shop: "王小虎夫妻店",
          shopId: "10333"
        }
      ]
    };
  }
};
</script>


