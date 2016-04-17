# 现代操作系统期中Project test

## 一、应用设计
### 1. 应用名
    钱柜（暂定）。
### 2. 功能概述
    大致上的功能有两条，一是对用户每月的支出和收入进行管理，二是用户可以设定一些目标，应用会督促你去完成。
### 3. 界面
<link href="//cdn.bootcss.com/bootstrap/4.0.0-alpha.2/css/bootstrap.css" rel="stylesheet">
<div class="row">
  <div class="col-xs-6 col-sm-3">
      ① 主界面<br />
      <img src="http://liuren.link/images/main.png" class="img-thumbnail" width = "270" height = "480" alt="主界面" align=center />
  </div>
  <div class="col-xs-6 col-sm-3">
      ② 支出收益详情<br />
      <img src="http://liuren.link/images/expence.png" class="img-thumbnail" width = "270" height = "480" alt="主界面" align=center />
  </div>
  <div class="col-xs-6 col-sm-3">
      ③ 添加支出收益<br />
      <img src="http://liuren.link/images/add.png" class="img-thumbnail" width = "270" height = "480" alt="主界面" align=center />
  </div>
  <div class="col-xs-6 col-sm-3">
      ④ 目标详情及设定<br />
      <img src="http://liuren.link/images/goal.png" class="img-thumbnail" width = "270" height = "480" alt="主界面" align=center />
  </div>
</div>
### 4. 操作逻辑
    * 点击主界面的“支出剩余”圆饼进入“支出收益详情”界面；
    * 点击“支出收益详情”界面的“添加新项”进入“添加支出收益”界面；
    * “添加支出收益”界面添加完新项后回到“支出收益详情”界面；
    * 点击主界面的右下角小加号进入“目标详情及设定”界面；
    * 更多细节将于之后更新。
### 5. 智能提醒
    * 由小灰灰补充。
### 6. 数据类型
类型名    |属性                             
---------|--------------------------------
收入类    |收入来源，收入数额，获得收入的时间    
支出类    |支出名称，支出数额，支出类别，支出时间 
支出类    |支出名称，支出数额，支出类别，支出时间 
目标类    |目标名称，目标所需的价格，计划完成时间 
收入list类|收入list，总收入                  
支出list类|支出list，总支出                  
目标list类|目标list                         
用户类    |3个list类(收入，支出，目标)，用户名  

## 二、规范
### 1. 变量名尽量用英语全称
    * 自己声明的一般变量以类似“apple”、“appleTree”方式命名；
    * 页面的变量名每个单词首字母大写；
    * 页面的变量名不以“NextPage”、“NextPage”、“MainPage2”等命名。
### 2. 合理地进行版本管理，每次完成一个小的功能就提交一下

## 三、工作日志
### 4月12日
    * 确定应用的功能；
    * 初定应用的界面和交互逻辑；
    * 确定分工。
