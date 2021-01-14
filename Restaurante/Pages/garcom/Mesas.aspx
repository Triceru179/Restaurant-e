<%@ Page Title="Garçom" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="Mesas.aspx.cs" Inherits="Pages_garcom_Mesas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">
    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Seleção de mesa </span>
        </div>
    </nav>
    <div class="pb-4"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <span class="btn-primary" style="cursor: pointer;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> Mesa disponível <br />
    <span class="btn-warning" style="cursor: pointer;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> Mesa ocupada <br />
    <span class="btn-danger" style="cursor: pointer;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> Aguardando pagamento <br />
    <div class="row mt-3">

        <div runat="server" class="col-12 text-center my-5" id="divNenhumaMesa">
            <div class="h3 my-5"> Nenhuma mesa registrada </div>
        </div>

        <asp:Repeater runat="server" ID="rptMesa">
            <ItemTemplate>
                <div class="col-6 col-md-4 col-lg-3 mb-3">
                    <asp:LinkButton runat="server" id="crdMesa" CssClass='<%# (Eval("com_foiFinalizada") is DBNull ? "btn-primary": Convert.ToInt32(Eval("com_foiFinalizada")) == 0 ? "btn-warning": Convert.ToInt32(Eval("com_foiPaga")) == 0 ? "btn-danger" : "btn-primary")+ " btn btn-block border-dark" %>'  OnClick="crdMesa_Click">
                        <asp:HiddenField runat="server" ID="hidId" Value='<%# DataBinder.Eval(Container.DataItem, "mes_id")%>' />
                        <asp:HiddenField runat="server" ID="hidComId" Value='<%# (DataBinder.Eval(Container.DataItem, "com_id") is DBNull ? "-1" : Convert.ToInt32(DataBinder.Eval(Container.DataItem, "com_id")) + "")%>' />
                        <div class="py-3 text-center ">
                            <asp:Label ID="lblIdentificacao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mes_identificacao") %>' />
                        </div>
                    </asp:LinkButton>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>