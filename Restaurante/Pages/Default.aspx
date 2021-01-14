<%@ Page Title="Página inicial" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <div class="jumbotron">
        <div class="container txt-jumbo">
            <div class="h3">
                Seja bem vindo, <span class="text-info"><% Response.Write(Session["nome"]); %></span>.
            </div>
        </div>
    </div>
    <% if ((Convert.ToInt32(Session["permissao"]) & Permissoes.admin) != 0 || (Convert.ToInt32(Session["permissao"]) & Permissoes.founder) != 0)
        {%>
    <div class="row pt-3">
        <div class="col-12 mb-1">
            Administrador
        </div>
        <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Card para ir aos funcionários ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
        <div class="col-12 col-md-4 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-center mt-2 bg-transparent">
                    Gerenciar funcionários
                </div>
                <div class="card-body text-white text-center fill-icon">
                    <img src="/icons/gerenciar-funcionario.svg" width="100" height="100" class="mb-3"/>
                    <a class="btn btn-block btn-primary" href="/Pages/admin/funcionario.aspx"> Ir para a página &nbsp; <i class="fa fa-external-link" aria-hidden="true"></i> </a>
                </div>
            </div>
        </div>
        <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Card para ir ao Dashboard ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
        <div class="col-12 col-md-4 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-white text-center mt-2 bg-transparent">
                    Dashboard
                </div>
                <div class="card-body text-center">
                    <img src="/icons/dashboard.svg" width="100" height="100" class="mb-3"/>
                    <a class="btn btn-block btn-primary" href="/Pages/relatorio/Dashboard.aspx"> Ir para a página &nbsp; <i class="fa fa-external-link" aria-hidden="true"></i> </a>
                </div>
            </div>
        </div>
        <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Card para ir as reclamações ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
        <div class="col-12 col-md-4 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-center mt-2 bg-transparent">
                    Visualizar reclamações
                </div>
                <div class="card-body text-white text-center">
                    <img src="/icons/gerenciar-reclamacao.svg" width="100" height="100" class="mb-3"/>
                    <a class="btn btn-block btn-primary" href="/Pages/admin/GerenciarReclamacao.aspx"> Ir para a página &nbsp; <i class="fa fa-external-link" aria-hidden="true"></i> </a>
                </div>
            </div>
        </div>
        <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Card para ir aos históricos ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
        <div class="col-12 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-center mt-2 bg-transparent">
                    Históricos
                </div>
                <div class="row p-3 pb-0">
                    <div class="col-12 col-md-6 col-lg-3 mb-2 text-white text-center">
                        <a class="btn btn-block btn-primary" href="/Pages/admin/HistoricoComanda.aspx">Comanda</a>
                    </div>
                    <div class="col-12 col-md-6 col-lg-3 mb-2 text-white text-center">
                        <a class="btn btn-block btn-primary" href="/Pages/admin/HistoricoPedido.aspx">Pedidos</a>
                    </div>
                    <div class="col-12 col-md-6 col-lg-3 mb-2 text-white text-center">
                        <a class="btn btn-block btn-primary" href="/Pages/admin/HistoricoProduto.aspx">Produtos</a>
                    </div>
                    <div class="col-12 col-md-6 col-lg-3 mb-2 text-white text-center">
                        <a class="btn btn-block btn-primary" href="/Pages/admin/HistoricoPagamento.aspx">Pagamentos</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <% } %>
    <% if ((Convert.ToInt32(Session["permissao"]) & Permissoes.gerente) != 0 || (Convert.ToInt32(Session["permissao"]) & Permissoes.founder) != 0)
        {%>
    <div class="row pt-3">
        <div class="col-12 mb-1">
            Gerente
        </div>
        <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Card para ir ao cardápio ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
        <div class="col-12 col-md-4 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-center mt-2 bg-transparent text-white">
                    Gerenciar cardápio
                </div>
                <div class="card-body text-center">
                    <img src="/icons/gerenciar-cardapio.svg" width="100" height="100" class="mb-3"/>
                    <a class="btn btn-block btn-primary" href="/Pages/gerente/Cardapio.aspx"> Ir para a página &nbsp; <i class="fa fa-external-link" aria-hidden="true"></i> </a>
                </div>
            </div>
        </div>
        <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Card para ir as mesas ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
        <div class="col-12 col-md-4 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-center mt-2 bg-transparent text-white">
                    Gerenciar mesas
                </div>
                <div class="card-body text-center">
                    <img src="/icons/gerenciar-mesa.svg" width="100" height="100" class="mb-3"/>
                    <a class="btn btn-block btn-primary" href="/Pages/gerente/GerenciarMesas.aspx"> Ir para a página &nbsp; <i class="fa fa-external-link" aria-hidden="true"></i> </a>
                </div>
            </div>
        </div>
        <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Card para ir as reclamações ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
        <div class="col-12 col-md-4 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-center mt-2 bg-transparent text-white">
                    Registrar reclamações
                </div>
                <div class="card-body text-center">
                    <img src="/icons/registrar-reclamacao.svg" width="100" height="100" class="mb-3"/>
                    <a class="btn btn-block btn-primary" href="/Pages/gerente/Reclamacao.aspx"> Ir para a página &nbsp; <i class="fa fa-external-link" aria-hidden="true"></i> </a>
                </div>
            </div>
        </div>
    </div>
    <% } %>
    <% if ((Convert.ToInt32(Session["permissao"]) & Permissoes.garcom) != 0 || (Convert.ToInt32(Session["permissao"]) & Permissoes.founder) != 0)
        {%>
    <div class="row pt-3">
        <div class="col-12 mb-1">
            Garçom
        </div>
        <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Card para ir as cardápio ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
        <div class="col-12 col-md-4 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-center mt-2 bg-transparent text-white">
                    Visualizar cardápio
                </div>
                <div class="card-body text-center">
                    <img src="/icons/visualizar-cardapio.svg" width="100" height="100" class="mb-3"/>
                    <a class="btn btn-block btn-primary" href="/Pages/garcom/VerProduto.aspx"> Ir para a página &nbsp; <i class="fa fa-external-link" aria-hidden="true"></i> </a>
                </div>
            </div>
        </div>

        <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Card para ir as comandas ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
        <div class="col-12 col-md-4 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-center mt-2 bg-transparent text-white">
                    Seleção de mesa
                </div>
                <div class="card-body text-center">
                    <img src="/icons/gerenciar-comanda.svg" width="100" height="100" class="mb-3"/>
                    <a class="btn btn-block btn-primary" href="/Pages/garcom/Mesas.aspx"> Ir para a página &nbsp; <i class="fa fa-external-link" aria-hidden="true"></i> </a>
                </div>
            </div>
        </div>
    </div>
    <% } %>
    <% if ((Convert.ToInt32(Session["permissao"]) & Permissoes.cozinha) != 0 || (Convert.ToInt32(Session["permissao"]) & Permissoes.founder) != 0)
        {%>
    <div class="row pt-3">
        <div class="col-12 mb-1">
            Cozinha
        </div>
        <div class="col-12 col-md-4 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-center mt-2 bg-transparent text-white">
                    Pedidos pendentes
                </div>
                <div class="card-body text-center">
                    <img src="/icons/pedido-pendente.svg" width="100" height="100" class="mb-3"/>
                    <a class="btn btn-block btn-primary" href="/Pages/cozinha/PedidoPendente.aspx"> Ir para a página &nbsp; <i class="fa fa-external-link" aria-hidden="true"></i> </a>
                </div>
            </div>
        </div>
    </div>
    <% } %>
    <% if ((Convert.ToInt32(Session["permissao"]) & Permissoes.caixa) != 0 || (Convert.ToInt32(Session["permissao"]) & Permissoes.founder) != 0)
        {%>
    <div class="row pt-3">
        <div class="col-12 mb-1">
            Caixa
        </div>
        <div class="col-12 col-md-4 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-center mt-2 bg-transparent text-white">
                    Pagamentos pendentes
                </div>
                <div class="card-body text-center">
                    <img src="/icons/registrar-pagamento.svg" width="100" height="100" class="mb-3"/>
                    <a class="btn btn-block btn-primary" href="/Pages/caixa/PagamentoPendente.aspx"> Ir para a página &nbsp; <i class="fa fa-external-link" aria-hidden="true"></i> </a>
                </div>
            </div>
        </div>
        <!--div class="col-12 col-md-4 mb-3">
            <div class="card bg-primary">
                <div class="card-header text-center mt-2 bg-transparent text-white">
                    Histórico de pagamento
                </div>
                <div class="card-body text-center">
                    <img src="/icons/historico-pagamento.svg" width="100" height="100" class="mb-3"/>
                    <a class="btn btn-block btn-primary" href="/Pages/caixa/HistoricoPagamento.aspx"> Ir para a página &nbsp; <i class="fa fa-external-link" aria-hidden="true"></i> </a>
                </div>
            </div>
        </div-->
    </div>
    <% } %>
    <% if (Convert.ToInt32(Session["permissao"]) == 0)
        {%>
    <div class="row pt-3">
        <div class="col-12 col-sm-8 offset-0 offset-sm-2 ">
            <div class="">
                <div class="h3 text-center px-5 pt-5 pb-2">
                    Nenhum cargo foi atribuído a sua conta
                </div>
                <div class="h6 text-center pb-3">
                    Converse com um administrador caso queira redefinir seu(s) cargo(s)
                </div>
            </div>
        </div>
    </div>
    <% } %>
</asp:Content>