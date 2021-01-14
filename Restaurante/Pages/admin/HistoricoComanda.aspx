<%@ Page Title="Administrador" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="HistoricoComanda.aspx.cs" Inherits="Pages_admin_HistoricoComanda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">
    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Histórico de comanda </span>
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
    <div class="card bg-primary mb-3">
        <div class="card-body">
            <table id="tblComanda" class="display" style="width:100%">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Finalizada</th>
                        <th>Foi paga</th>
                        <th>Removida</th>
                        <th>Data/hora criação</th>
                        <th>Id funcionário</th>
                        <th>Id Mesa</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptComanda">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "com_id") %>' /></td>
                                <td><%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "com_foiFinalizada")) == 0 ? "Não" : "Sim" %></td>
                                <td><%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "com_foiPaga")) == 0 ? "Não" : "Sim" %></td>
                                <td><%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "com_disabled")) == 0 ? "Não" : "Sim" %></td>
                                <td><asp:Label ID="lblDataHora" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "com_dthrCriacao") %>' /></td>
                                <td><asp:Label ID="lblFuncionario" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fun_id") %>' /></td>
                                <td><asp:Label ID="lblMesa" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mes_id") %>' /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>

    <div class="card bg-primary">
        <div class="card-body">
            <table id="tblComanda2" class="display" style="width:100%">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Motivo</th>
                        <th>Data/hora cancelada</th>
                        <th>Id comanda</th>
                        <th>Id funcionário</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptComandaCancelada">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "coc_id") %>' /></td>
                                <td><asp:Label ID="lblMotivo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "coc_motivo") %>' /></td>
                                <td><asp:Label ID="lblDthr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "coc_dthrCancelada") %>' /></td>
                                <td><asp:Label ID="lblComId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "com_id") %>' /></td>
                                <td><asp:Label ID="lblFunId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fun_id") %>' /></td>
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