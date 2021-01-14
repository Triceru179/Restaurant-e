<%@ Page Title="Administrador" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="GerenciarReclamacao.aspx.cs" Inherits="Pages_admin_GerenciarReclamacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">
    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Visualizar reclamações</span>
        </div>
    </nav>
    <div class="pb-4"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <div class="row">
        <div class="col-12 col-md-6 col-lg-3 text-center">
            <div class="card bg-primary p-3 mb-3">
                <asp:label runat="server" id="lblQntReclamacao" cssclass="h4 mb-0" text="0" />
                <br />
                Reclamações pendentes
            </div>
        </div>
    </div>
    <!-- Cards que contém todas as reclamações registradas -->
    <div class="row mt-3">
        <div runat="server" class="col-12 text-center my-5" id="divNenhumaReclamacao">
            <div class="h3 my-5">
                Nenhuma reclamação registrada
            </div>
        </div>

        <asp:Repeater runat="server" ID="rptReclamacao">
            <ItemTemplate>
                <div class="col-12 col-md-4 mb-3">
                    <div class="card bg-primary px-1 mx-0">
                        <div class="card-body">
                            Reclamação&nbsp;<asp:Label runat="server" ID="hidRecIdent" Text='<%# Container.ItemIndex + 1 %>'/>
                            <hr />
                            <%-- Guarda em campos hidden as informações de ID da reclamação (para usar no CodeBehind) --%>
                            <asp:HiddenField runat="server" ID="hidIdRec" Value='<%# DataBinder.Eval(Container.DataItem, "rec_id") %>' />
                            <label>Funcionário que registrou:</label><br />
                            <asp:Label runat="server" ID="Label1" Text='<%# DataBinder.Eval(Container.DataItem, "fun_id") + " - " + DataBinder.Eval(Container.DataItem, "fun_nome") %>' />
                            <hr />
                            <label>Categoria:</label><br />
                            <asp:Label runat="server" ID="lblCategoria" Text='<%# DataBinder.Eval(Container.DataItem, "rec_categoria") %>' />
                            <hr />
                            <label>Descrição:</label><br />
                            <asp:Label runat="server" ID="lblDescricao" Text='<%# DataBinder.Eval(Container.DataItem, "rec_descricao") %>' />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>