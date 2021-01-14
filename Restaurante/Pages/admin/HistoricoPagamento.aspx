<%@ Page Title="Administrador" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="HistoricoPagamento.aspx.cs" Inherits="Pages_admin_HistoricoPagamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">
    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Histórico de pagamento </span>
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
            <table id="tblPagamento" class="display" style="width:100%">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Valor</th>
                        <th>Gorjeta</th>
                        <th>Descrição</th>
                        <th>Data/hora</th>
                        <th>Id funcionário</th>
                        <th>Id pedido</th>
                        <th>Id comanda</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptPagamento">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "cai_id") %>' /></td>
                                <td><asp:Label runat="server" ID="lblValorTotal" Text='<%# String.Format("R${0:0.00}", Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cai_valorTotal"))) %>'/></td>
                                <td><asp:Label runat="server" ID="lblGorjeta" Text='<%# String.Format("R${0:0.00}", Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cai_gorjeta"))) %>'/></td>
                                <td><asp:Label ID="lblDescricao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "cai_descricao") %>' /></td>
                                <td><asp:Label ID="lblDataHora" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "cai_dthrPagamento") %>' /></td>
                                <td><asp:Label ID="lblIdFuncionario" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fun_id") %>' /></td>
                                <td><asp:Label ID="lblIdPedido" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ped_id") %>' /></td>
                                <td><asp:Label ID="lblIdComanda" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "com_id") %>' /></td>
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