<%@ Page Title="Cozinha" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="PedidoPendente.aspx.cs" Inherits="Pages_cozinha_PedidoPendente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">
    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Pedidos pendentes </span>
        </div>
    </nav>
    <div class="pb-4"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <div class="row mb-2">
        <div class="col-12 col-md-6 col-lg-4">
            <asp:LinkButton runat="server" ID="btnAtualizar" CssClass="btn btn-block btn-primary" Text="Atualizar pedidos" OnClick="btnAtualizar_Click"/>
        </div>
    </div>

    <!-- Tabela com todos os pedidos -->
    <div class="card bg-primary">
        <div class="card-body">
            <table id="tblPedido" class="display" style="width:100%">
                <thead>
                    <tr>
                        <th>Data/Hora</th>
                        <th></th>
                        <th></th>
                        <th>Pedido</th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptPedidoPendente">
                        <ItemTemplate>
                            <tr>
                                <asp:HiddenField runat="server" ID="hidIdPnp" Value='<%# DataBinder.Eval(Container.DataItem, "pnp_id") %>' />
                                <asp:HiddenField runat="server" ID="hidProduto" Value='<%# DataBinder.Eval(Container.DataItem, "pro_nome") %>' />
                                <asp:HiddenField runat="server" ID="hidIdPed" Value='<%# DataBinder.Eval(Container.DataItem, "ped_id") %>' />
                                <asp:HiddenField runat="server" ID="hidQntPnp" Value='<%# DataBinder.Eval(Container.DataItem, "pnp_quantidade") %>' />
                                <asp:HiddenField runat="server" ID="hidValorPro" Value='<%# DataBinder.Eval(Container.DataItem, "pro_valor") %>' />

                                <td><asp:Label ID="lblData" runat="server" style="font-weight: 600;" Text='<%# DataBinder.Eval(Container.DataItem, "ped_dthrCriacao") %>' /></td>
                                <td><asp:Label ID="lblMesa" runat="server" Text='<%# "Mesa " + DataBinder.Eval(Container.DataItem, "mes_identificacao") %>' /></td>
                                <td><asp:Label ID="lblPedido" runat="server" Text='<%# "Pedido " + DataBinder.Eval(Container.DataItem, "mes_id") %>' /></td>
                                <td><asp:Label ID="lblPnp" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pnp_quantidade") + "x " + DataBinder.Eval(Container.DataItem, "pro_nome") + ((DataBinder.Eval(Container.DataItem, "pnp_observacao").ToString()).Equals("") ? "" : "(" + DataBinder.Eval(Container.DataItem, "pnp_observacao") + ")" )%>' /></td>
                                <td>
                                    <asp:LinkButton runat="server" UseSubmitBehavior="False" CssClass="btn btn-outline-danger col-12" ID="btnExcluir" OnClick="btn_excluir"> × </asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" UseSubmitBehavior="False" CssClass="btn btn-primary col-12" ID="btnFinalizar" OnClick="btn_finalizar"> Finalizar </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>

    <!-- Modal finalizar -->
    <div class="modal fade" id="mdlFinalizar" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <label>Finalizar item</label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField runat="server" id="hidIdFinalizar"/>
                    <asp:HiddenField runat="server" id="hidIdPedFinalizar"/>
                    Finalizar o item <asp:Label runat="server" ID="lblProdutoFinalizar" CssClass="text-warning"/> na <asp:Label runat="server" ID="lblMesaFinalizar" CssClass="text-warning"/>?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:Button runat="server" id="btnFinalizar" CssClass="btn btn-success" Text="Entregar" type="submit" OnClick="btnConfirmarFinalizar_Click"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal excluir -->
    <div class="modal fade" id="mdlExcluir" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <label>Excluir item</label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField runat="server" id="hidIdExcluir"/>
                    <asp:HiddenField runat="server" ID="hidIdPedExcluir"/>
                    <asp:HiddenField runat="server" ID="hidValorProExcluir"/>
                    <asp:HiddenField runat="server" ID="hidQntPnpExcluir"/>
                    Excluir o item <asp:Label runat="server" ID="lblProdutoExcluir" CssClass="text-warning"/> na <asp:Label runat="server" ID="lblMesaExcluir" CssClass="text-warning"/>?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:Button runat="server" id="btnExcluir" CssClass="btn btn-danger" Text="Excluir" type="submit" OnClick="btnConfirmarExcluir_Click"/>
                </div>
            </div>
        </div>
    </div>

    <script>
    $(document).ready(function () {
            $('#tblPedido').DataTable({
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

        if (<%= finalizar ? "true" : "false" %>)
        {
            $("#mdlFinalizar").modal('show');
        }

        if (<%= excluir ? "true" : "false" %>)
        {
            $("#mdlExcluir").modal('show');
        }

    </script>
</asp:Content>