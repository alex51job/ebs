<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Hunli/Hunli.Master" AutoEventWireup="true" CodeBehind="taginput.aspx.cs" Inherits="ebs.MoKuai_Hunli.taginput" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../tokenfield/css/bootstrap-tokenfield.min.css" rel="stylesheet" type="text/css" />
    <script src="../tokenfield/bootstrap-tokenfield.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function addtips() {

            var txt = $('#fwxm').val() + " " + $('#bz1').val() + " " + $('#bz2').val();
            $('#tokenfield').tokenfield('createToken', txt);
    }
    </script>
    <script type="text/javascript">
        $(function () {
            $('.tokenfield').tokenfield();
            //$('.tokenfield').tokenfield('readonly');
        });
        $(function () {
            $("#addBt").popover({
                html: true,
                container: 'body',
                placement:"bottom"
            }); 
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="btn btn-default">add</div>
<div class="row">
<div class="col-lg-6">
<div class=" input-group">
<input type="text" class="form-control tokenfield" id="tokenfield"  value="" />
<span class="input-group-btn">
                    <div class="btn btn-default" id="addBt"  tabindex="0"  role="button" data-trigger="focus" 
                     data-container="body" data-toggle="popover" data-content="
                    <div class=' form-group-sm'>
                    服务项目：<input type='text' class='form-control' style='width:200px' id='fwxm'/>
                      备注1：<input type='text'  class='form-control' style='width:200px' id='bz1'/>
                      <small>可输入项目的类别等</small><br />
                      备注2：<input type='text'  class='form-control' style='width:200px' id='bz2'/>
                      <small>可输入项目的券号等</small><br />
                      <div class='btn btn-danger btn-sm'  onclick='addtips()'>确定</div>
                        
                    </div>
                     "
                    >增加</div>
                  </span>
</div>
</div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
