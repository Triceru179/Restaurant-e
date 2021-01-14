<%@ Page Title="Administrador" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="HistoricoProduto.aspx.cs" Inherits="Pages_admin_HistoricoProduto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">
    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Histórico de produto </span>
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

    <div class="row mb-2">
        <div class="col-12">
            Histórico de produtos vendidos
        </div>
    </div>

    <div class="card bg-primary">
        <div class="card-body">
            <table id="tblPnp" class="display" style="width:100%">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Nome</th>
                        <th>Quantidade</th>
                        <th>Valor total</th>
                        <th>Id pedido</th>
                        <th>Observação</th>
                        <th>Data/hora</th>
                        <th>Foi feito</th>
                        <th>Removido</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptPnp">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pnp_id") %>' /></td>
                                <td><asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pro_nome") %>' /></td>
                                <td><asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pnp_quantidade") %>'></asp:Label></td>
                                <td><asp:Label runat="server" Text='<%# String.Format("R${0:0.00}", Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pnp_valor")) * Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pnp_quantidade"))) %>'/></td>
                                <td><asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ped_id") %>' /></td>
                                <td><asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pnp_observacao") %>' /></td>
                                <td><asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pnp_dthrCozinha") %>' /></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "pnp_foiFeito").ToString().Equals("True") ? "Sim" : "Não" %></td>
                                <td><%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "pnp_disabled")) == 0 ? "Não" : Convert.ToInt32(DataBinder.Eval(Container.DataItem, "pnp_disabled")) == 1 ? "Sim" : "Cancelado" %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
    <div class="row pt-3">
        <div class="col-12">
            Histórico de todos produtos já cadastrados
        </div>
    </div>
    <div class="card bg-primary">
        <div class="card-body">
            <table id="tblProdutos" class="display" style="width:100%">
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Descrição</th>
                        <th>Valor</th>
                        <th>É complemento?</th>
                        <th>Disponível</th>
                        <th>Removido</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptProduto">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Label ID="lblNome" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pro_nome") %>' /></td>
                                <td><asp:Label runat="server" ID="lblDescricao" Text='<%# DataBinder.Eval(Container.DataItem, "pro_descricao") %>'></asp:Label></td>
                                <td><asp:Label runat="server" ID="lblValor" Text='<%# String.Format("R${0:0.00}", Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pro_valor"))) %>'/></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "pro_complemento").ToString().Equals("True") ? "Sim" : "Não" %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "pro_disponivel").ToString().Equals("True") ? "Sim" : "Não" %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "pro_disabled").ToString().Equals("True") ? "Sim" : "Não" %></td>
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