<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Kehu/Kehu.Master" AutoEventWireup="true"
    CodeBehind="KehuFollow.aspx.cs" Inherits="ebs.MoKuai_Kehu.KehuFollow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">客户
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="col-sm-12 col-md-12 col-lg-12 ">
   <ul class="nav nav-tabs">
          <%= MakeTabs(ID,SW,HL)%>
    </ul>
</div>
    <div class="clearfix">
    </div>
    <div class="col-sm-10 col-md-10 col-lg-10 col-lg-offset-1">
        <!-- 客服跟踪 --><br />
        <div class="panel panel-default">
            <a href="#beizhu" class="panel-heading" data-toggle="collapse">客服部跟踪</a>
            <div id="beizhu" class="panel-collapse panel-body collapse in">
              <div class=" col-lg-12 table-responsive">
                <asp:GridView ID="gwActions" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                    ShowFooter="true" CssClass="table table-bordered table-condensed" OnRowDeleting="gwActions_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="更新日期">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                            <ItemTemplate>
                                <asp:Label ID="lblPLANT" runat="server" Text='<%# Bind("CreatedDt", "{0:yyyy-MM-dd hh:mm tt}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" Width="15%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="客服名">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                            <ItemTemplate>
                                <asp:Label ID="lblKfName" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="客服部行动\结果">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                            <ItemTemplate>
                                <asp:Label ID="lblSVW" runat="server" Text='<%# Bind("ActionContent") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtActions" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Add" runat="server"
                                    ErrorMessage="*必填" Display="Dynamic" ControlToValidate="txtActions" ForeColor="Red"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Left" />
                            <FooterStyle Wrap="false" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <FooterTemplate>
                                <asp:LinkButton ID="lnkAddNew" runat="server" OnClick="lnkAddNew_Click" ValidationGroup="Add">Add</asp:LinkButton>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDel" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="Delete" OnClientClick="javascript:return confirm('are you sure to delete?');"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" HorizontalAlign="Center" />
                            <FooterStyle Wrap="false" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle Wrap="False" />
                    <RowStyle Wrap="False" />
                </asp:GridView>
                </div>
            </div>
        </div>
        <!-- 销售跟踪 -->
        <div class="panel panel-default">
            <a href="#SalesFollow" class="panel-heading" data-toggle="collapse">销售部跟踪</a>
            <div id="SalesFollow" class="panel-collapse panel-body collapse in">
              <div class=" col-lg-12 table-responsive">
                <asp:GridView ID="gwActionsSales" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                    ShowFooter="true" CssClass="table table-bordered table-condensed" OnRowDeleting="gwActionsSales_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="更新日期">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                            <ItemTemplate>
                                <asp:Label ID="lblXiaoshou" runat="server" Text='<%# Bind("CreatedDt", "{0:yyyy-MM-dd hh:mm tt}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" Width="15%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="销售名">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                            <ItemTemplate>
                                <asp:Label ID="lblPLANT" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售行动\结果">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                            <ItemTemplate>
                                <asp:Label ID="lblSVW" runat="server" Text='<%# Bind("ActionContent") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtActionsSales" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="AddSales" runat="server"
                                    ErrorMessage="*必填" Display="Dynamic" ControlToValidate="txtActionsSales" ForeColor="Red"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Left" />
                            <FooterStyle Wrap="false" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <FooterTemplate>
                                <asp:LinkButton ID="lnkAddNew" runat="server" OnClick="lnkAddNewSales_Click" ValidationGroup="AddSales">Add</asp:LinkButton>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDel" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="Delete" OnClientClick="javascript:return confirm('are you sure to delete?');"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" HorizontalAlign="Center" />
                            <FooterStyle Wrap="false" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle Wrap="False" />
                    <RowStyle Wrap="False" />
                </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
