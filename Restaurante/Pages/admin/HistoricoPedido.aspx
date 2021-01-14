<%@ Page Title="Administrador" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="HistoricoPedido.aspx.cs" Inherits="Pages_admin_HistoricoPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">
    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Histórico de pedido </span>
        </div>
    </nav>
    <div class="pb-4"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <div class="row">
        <div class="col-auto">
            <div class="card bg-primary py-2 px-3 mb-3">
                <div>
                    Informações contidas referentes ao ano de <asp:TextBox runat="server" id="txtHistoricoAno" type="number" min="1900" CssClass="ml-2 txtbox" OnTextChanged="txtHistoricoAno_TextChanged" AutoPostBack="true"/>
                </div>
            </div>
        </div>
    </div>
    <div class="card bg-primary">
        <div class="card-body">
            <table id="tblPedido" class="display" style="width:100%">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Valor do pedido</th>
                        <th>Preparo solicitado</th>
                        <th>Foi entregue</th>
                        <th>Removido</th>
                        <th>Data/hora Criação</th>
                        <th>Id comanda</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptPedido">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ped_id") %>' /></td>
                                <td><asp:Label runat="server" ID="lblValorPedido" Text='<%# String.Format("R${0:0.00}", Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ped_valor"))) %>'/></td>
                                <td><%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ped_solicitarPreparo")) == 0 ? "Não" : "Sim" %></td>
                                <td><%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ped_foiEntregue")) == 0 ? "Não" : "Sim" %></td>
                                <td><%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ped_disabled")) == 0 ? "Não" : "Sim" %></td>
                                <td><asp:Label ID="lblDataHora" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ped_dthrCriacao") %>' /></td>
                                <td><asp:Label ID="lblComanda" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "com_id") %>' /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            $('.display').DataTable({
                "order": [[0, "desc"]],
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
    </script>
</asp:Content>