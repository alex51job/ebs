<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageTeam.aspx.cs" Inherits="ebs.ManageTeam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">
My Team
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="col-sm-4 col-md-4" id='Div1'>
<div class="btn-toolbar">
            <a id="A1" href="#myModal" data-toggle="modal" class="btn btn-primary btn-sm"><i
                class="fa fa-plus-circle"></i>&nbsp;New Team</a>
        </div>
</div>
<div class=" clearfix"></div>
   <div class=" modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                    aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    ×</button>
                                <h3 id="myModalLabel">
                                    Build My Team
                                </h3>
                            </div>
                            <div class="modal-body" style="padding: 5px;">
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="panel panel-default">
                                                <div class="panel-heading no-collapse ">
                                                    <div class="form-group has-success has-feedback">
                                                        <div class="input-group">
                                                            <span class="input-group-addon" id="basic-addon2">ECN team name </span>
                                                            <input type="text" class="form-control" placeholder="Input team name" id="txtTeamName"
                                                                runat="server" aria-describedby="basic-addon2">
                                                        </div>
                                                    </div>
                                                </div>
                                                <table class="table table-bordered table-striped">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                角色
                                                            </th>
                                                            <th>
                                                                姓名
                                                            </th>
                                                          
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                销售
                                                            </td>
                                                            <td>
                                                                <select id="dd_MEIE" class="small search-people form-control" runat="server" style="width: 180px">
                                                                </select>
                                                            </td>
                                                           
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                支持
                                                            </td>
                                                            <td>
                                                                <select id="dd_MEBP" class="small search-people form-control" runat="server" style="width: 180px">
                                                                </select>
                                                            </td>
                                                         
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                售后
                                                            </td>
                                                            <td>
                                                                <select id="dd_MEProcess" class="small search-people form-control" runat="server" style="width: 180px">
                                                                </select>
                                                            </td>
                                                        </tr>
                                                   
                                                    </tbody>
                                                </table>
                                            </div>
                                            <span class="label alert-danger" style="font-size: 12px; font-weight: normal" id="ErrMsg">
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                                <asp:LinkButton ID="btnSave" CssClass="lbtSubmit btn btn-danger" runat="server" >Submit</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class=" modal fade" id="editModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                    aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <%-- <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    ×</button>--%>
                                <h3 id="H1">
                                    Edit My Team</h3>
                            </div>
                            <div class="modal-body" style="padding: 5px;">
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="panel panel-default">
                                                <div class="panel-heading no-collapse ">
                                                    <b>Team Name </b>: <span id="updTeamName"></span>
                                                </div>
                                                <div class="main-content">
                                                    <div class="row">
                                                        <div class=" col-lg-12 form-horizontal ">
                                                            <br>
                                                            <div class="form-group form-group-sm col-lg-12 ">
                                                                <label class="col-sm-3 control-label">
                                                                    Select a Role</label>
                                                                <div class="col-sm-8">
                                                                    <select id="Role" class="form-control" runat="server" onchange="">
                                                                        <option></option>
                                                                        <option>销售</option>
                                                                        <option>支持</option>
                                                                        <option>售后</option>
                                                                      
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="form-group form-group-sm col-lg-12 ">
                                                                <label class="col-sm-3 control-label" id="upEng">
                                                                    姓名</label>
                                                                <div class="col-sm-8">
                                                                    <select id="sel_Eng" class="small  search-people" style="width: 100%" runat="server">
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <span class="label alert-danger" style="font-size: 12px; font-weight: normal" id="errMsg_upd">
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <input type="hidden" id="hdTeamID" value="" />
                                <button class="btn btn-default" id="close" runat="server" >
                                    Close & Reflesh</button>
                                <div class=" btn btn-danger" id="lbUpdMyTeam" >
                                    Submit</div>
                            </div>
                        </div>
                    </div>
                </div>
               <br />
                        <div class="col-sm-4 col-md-4" id='kk'>
                            <div class="panel panel-default">
                                <div class="panel-heading no-collapse">
                                     我的第一个队伍
                                    <span class="panel-icon pull-right" style="cursor: pointer"><i class="fa fa-pencil"
                                        onclick=""></i>&nbsp;&nbsp;
                                        <i class="fa fa-trash-o" style="cursor: pointer" onclick='tt'>
                                        </i></span>
                                </div>
                                <table class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>
                                                角色
                                            </th>
                                            <th>
                                                姓名
                                            </th>
                                          
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                销售
                                            </td>
                                            <td>
                                               比尔
                                            </td>
                                          
                                        </tr>
                                        <tr>
                                            <td>
                                                支持
                                            </td>
                                            <td>
                                                拉里
                                            </td>
                                         
                                        </tr>
                                        <tr>
                                            <td>
                                                售后
                                            </td>
                                            <td>
                                                库克
                                            </td>
                                            
                                        </tr>
                                      
                                  
                                  
                                    </tbody>
                                </table>
                            </div>
                        </div>
                
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
