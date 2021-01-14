<%@ Page Title="Administrador" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="RelatorioProduto.aspx.cs" Inherits="Pages_Relatorio_RelatorioProduto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">
    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Relatórios de produto </span>
        </div>
    </nav>
    <div class="pb-4"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <div class="row my-2">
        <div class="col-12 print">
            <span class="h4">
                Vendas de produto
                <br />
                Data: <% Response.Write(DateTime.Now); %>
            </span>
        </div>
        <div class="col-auto">
            <div class="card bg-primary py-2 px-3 mb-3">
                <div>
                    Informações contidas referentes ao ano de <asp:TextBox runat="server" id="txtHistoricoAno" type="number" min="1900" CssClass="ml-2 txtbox" OnTextChanged="txtHistoricoAno_TextChanged" AutoPostBack="true"/>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-12 col-md-6 mb-3">
            <div class="card bg-primary">
                <div class="card-header">
                    <div class="text-center">
                        Vendas por item (5 mais vendidos)
                    </div>
                </div>
                <div class="card-body">
                    <asp:Literal runat="server" ID="ltlPorProdutoQuantidade" Text="Nenhuma informação encontrada"/>
                </div>
            </div>
        </div>
        <div class="col-12 col-md-6 mb-3">
            <div class="card bg-primary">
                <div class="card-header">
                    <div class="text-center">
                        Vendas por item (5 mais rentáveis)
                    </div>
                </div>
                <div class="card-body">
                    <asp:Literal runat="server" ID="ltlPorProdutoValor" Text="Nenhuma informação encontrada"/>
                </div>
            </div>
        </div>
    </div>

    <div class="card bg-primary">
        <div class="row m-2">
            <div class="col-12">
                Todos produtos vendidos no ano de <% Response.Write(DateTime.Now.Year); %>
            </div>
        </div>
        <div class="card-body">
            <table id="tblProdutos" class="display" style="width:100%">
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Quantidade vendida</th>
                        <th>Valor total</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptProduto">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Label ID="lblNome" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nome") %>' /></td>
                                <td><asp:Label runat="server" ID="lblQuantidade" Text='<%# DataBinder.Eval(Container.DataItem, "quantidade") %>'></asp:Label></td>
                                <td><asp:Label runat="server" ID="lblValor" Text='<%# String.Format("R${0:0.00}",  DataBinder.Eval(Container.DataItem, "valor")) %>'></asp:Label></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
    <div class="row mt-2">
        <div class="col-12 col-md-6 col-lg-4 mb-3">
            <div class="card bg-primary p-3 txt">
                <div class="card-header text-center mt-2 bg-transparent">
                    Imprimir esta página
                </div>
                <div class="card-body text-white text-center">
                    <a class="btn btn-primary btn-block" onclick="imprimir()">Imprimir</a>
                </div>
            </div>
        </div>
    </div>
    <script>
        Chart.defaults.global.defaultFontColor = '#e2e2e2';

        $(document).ready(function () {
            $('#tblProdutos').DataTable({
                "order": [1, "desc"],
                "scrollX": true,
                "language": { 
                    "lengthMenu": "Exibindo _MENU_ registros por página",
                    "zeroRecords": "Nenhum registro encontrado",
                    "info": "Exibindo de _START_ até _END_ de _TOTAL_ registros",
                    "infoEmpty": "Nenhum registro disponível",
                    "infoFiltered": "(Filtrado de _MAX_ registros totais)",
                    "paginate": {
                        "previous": "Anterior",
                        "next": "Próxima"
                    },
                },
                "oLanguage": {
                    "sSearch": "Pesquisa"
                }
            });
        });

        function beforePrint() {
            for (const id in Chart.instances) {

                Chart.plugins.register({
                    beforeDraw: function (chartInstance) {
                        var ctx = chartInstance.chart.ctx;
                        ctx.fillStyle = "#474f69";
                        ctx.fillRect(0, 0, chartInstance.chart.width, chartInstance.chart.height);
                    }
                });

                Chart.instances[id].resize()
            }
        }

        if (window.matchMedia) {
            let mediaQueryList = window.matchMedia('print')
            mediaQueryList.addListener((mql) => {
                if (mql.matches) {
                    beforePrint()
                }
            })
        }

        window.onbeforeprint = beforePrint

        function imprimir() {
            var tempTitle = document.title;
            document.title = "Relatório de produtos";
            window.print();
            document.title = tempTitle;
        }
    </script>
</asp:Content>