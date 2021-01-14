<%@ Page Title="Administrador" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="Historico.aspx.cs" Inherits="Pages_admin_Historico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">
    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Seleção de histórico </span>
        </div>
    </nav>
    <div class="pb-4"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <div class="row pt-3">
        <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Card para ir ao histórico de comanda ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
        <div class="col-12 col-md-6 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-center mt-2 bg-transparent">
                    Histórico de comandas
                </div>
                <div class="card-body text-white text-center">
                    <a class="btn btn-block btn-primary" href="/Pages/admin/HistoricoComanda.aspx"> Ir para a página &nbsp; <i class="fa fa-external-link" aria-hidden="true"></i> </a>
                </div>
            </div>
        </div>
        <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Card para ir ao histórico de pedido ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
        <div class="col-12 col-md-6 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-center mt-2 bg-transparent">
                    Histórico de pedidos
                </div>
                <div class="card-body text-white text-center">
                    <a class="btn btn-block btn-primary" href="/Pages/admin/HistoricoPedido.aspx"> Ir para a página &nbsp; <i class="fa fa-external-link" aria-hidden="true"></i> </a>
                </div>
            </div>
        </div>
        <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Card para ir ao histórico de produto ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
        <div class="col-12 col-md-6 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-center mt-2 bg-transparent">
                    Histórico de produtos
                </div>
                <div class="card-body text-white text-center">
                    <a class="btn btn-block btn-primary" href="/Pages/admin/HistoricoProduto.aspx"> Ir para a página &nbsp; <i class="fa fa-external-link" aria-hidden="true"></i> </a>
                </div>
            </div>
        </div>
        <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Card para ir ao histórico de pagamentos ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
        <div class="col-12 col-md-6 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-center mt-2 bg-transparent">
                    Histórico de pagamentos
                </div>
                <div class="card-body text-white text-center">
                    <a class="btn btn-block btn-primary" href="/Pages/admin/HistoricoPagamento.aspx"> Ir para a página &nbsp; <i class="fa fa-external-link" aria-hidden="true"></i> </a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>