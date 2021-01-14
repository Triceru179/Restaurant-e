<%@ Page Title="Administrador" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Pages_admin_Relatorio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">
    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Dashboard </span>
        </div>
    </nav>
    <div class="pb-4"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <!-- Parte superior, informações numéricas -->
    <div class="row mt-2">
        <div class="col-12 print">
            <span class="h4">
                Dashboard <br />
                Data: <% Response.Write(DateTime.Now); %>
            </span>
        </div>
        <div class="col-12 col-md-4 text-center">
            <div class="card bg-primary px-3 pt-3 mb-3">
                <div class="mb-2">Entrada ( Dia <span class="text-info"><% Response.Write(DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year); %> </span>)</div>
                <asp:label runat="server" id="lblValorDia" cssclass="h4 m-0 txtVenda" text="0" />
                <br />
            </div>
        </div>
        <div class="col-12 col-md-4 text-center">
            <div class="card bg-primary px-3 pt-3 mb-3">
                <div class="mb-2">Entrada ( Mês <span class="text-info"><% Response.Write(DateTime.Now.Month + "/" + DateTime.Now.Year); %> </span>)</div>
                <asp:Label runat="server" ID="lblValorMes" CssClass="h4 mb-0 txtVenda" Text="0"/>
                <br />
            </div>
        </div>
        <div class="col-12 col-md-4 text-center">
            <div class="card bg-primary px-3 pt-3 mb-3">
                <div class="mb-2">Entrada ( Ano <span class="text-info"><% Response.Write(DateTime.Now.Year); %> </span> )</div>
                <asp:Label runat="server" ID="lblValorAno" CssClass="h4 mb-0 txtVenda" Text="0"/>
                <br />
            </div>
        </div>
    </div>

    <!-- Parte central, informações em gráficos -->
    <div class="row">
        
        <div class="col-12 col-lg-8 offset-0 offset-lg-2 mb-5">
            <div class="card bg-primary">
                <div class="card-header">
                    <div class="text-center">
                        Renda
                    </div>
                </div>
                <div class="card-body">

                    <asp:DropDownList runat="server" ID="lstRendaDataFiltro" AutoPostBack="true" OnSelectedIndexChanged="lstRendaDataFiltro_SelectedIndexChanged">
                        <asp:ListItem Text="Por semana" Value="1"/>
                        <asp:ListItem Text="Por mês" Value="2" Selected="True"/>
                        <asp:ListItem Text="Por ano" Value="3"/>
                    </asp:DropDownList>

                    <asp:TextBox type="date" runat="server" ID="txtDataRenda" Visible="false" OnTextChanged="txtDataRenda_TextChanged" AutoPostBack="true"/>
                    <asp:TextBox runat="server" id="txtAnoRenda" type="number" min="1900" CssClass="txtbox" OnTextChanged="txtAnoRenda_TextChanged" AutoPostBack="true"/>
                    <div class="col-12"/>
                    <asp:Literal runat="server" ID="ltlRendaPorMes" Text="Nenhuma informação encontrada"/>
                </div>
            </div>
        </div>
    </div>

    <div class="row mt-3">
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

        <div class="col-12 col-md-6 col-lg-4 mb-3">
            <div class="card bg-primary p-3 txt">
                <div class="card-header text-center mt-2 bg-transparent">
                    Relatórios de comandas
                </div>
                <div class="card-body text-white text-center">
                    <a class="btn btn-primary btn-block" href="/Pages/relatorio/RelatorioComanda.aspx">Ir para a página</a>
                </div>
            </div>
        </div>

        <div class="col-12 col-md-6 col-lg-4 mb-3">
            <div class="card bg-primary p-3 txt">
                <div class="card-header text-center mt-2 bg-transparent">
                    Relatórios de produtos
                </div>
                <div class="card-body text-white text-center">
                    <a class="btn btn-primary btn-block" href="/Pages/relatorio/RelatorioProduto.aspx">Ir para a página</a>
                </div>
            </div>
        </div>
        

        <div class="col-12 txt" style="visibility: hidden">
            aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
        </div>
    </div>

    <script>
        Chart.defaults.global.defaultFontColor = '#e2e2e2';

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
                else{
                    afterPrint()
                }
            })
        }

        window.onbeforeprint = beforePrint

        function imprimir() {
            var tempTitle = document.title;
            document.title = "Dashboard";
            window.print();
            document.title = tempTitle;
        }

    </script>
</asp:Content>