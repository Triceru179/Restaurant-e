<%@ Page Title="Caixa" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="PagamentoPendente.aspx.cs" Inherits="Pages_caixa_PagamentoPendente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">
    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Pagamentos pendentes </span>
        </div>
    </nav>
    <div class="pb-4"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <div class="row mb-2">
        <div class="col-12 col-md-6 col-lg-4 mb-2">
            <asp:LinkButton runat="server" ID="btnAtualizar" CssClass="btn btn-block btn-primary" Text="Atualizar pagamentos" OnClick="btnAtualizar_Click"/>
        </div>
        <div class="col-12 col-md-6 col-lg-4">
            <asp:LinkButton runat="server" ID="btnPagamentoNaoAgendado" CssClass="btn btn-block btn-primary" Text="Adicionar pagamento não agendado" OnClick="btn_PagamentoNaoAgendado"/>
        </div>
        <div class="col-12 col-md-6 col-lg-4 py-1">
            <asp:Label runat="server" CssClass="" ID="res"  Text=""/>
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
                        <th>Valor</th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptPagamentoPendente">
                        <ItemTemplate>
                            <tr>
                                <asp:HiddenField runat="server" ID="hidIdPed" Value='<%# DataBinder.Eval(Container.DataItem, "ped_id") %>' />
                                <asp:HiddenField runat="server" ID="hidIdCom" Value='<%# DataBinder.Eval(Container.DataItem, "com_id") %>' />
                                <asp:HiddenField runat="server" ID="hidPedValor" Value='<%# DataBinder.Eval(Container.DataItem, "ped_valor") %>' />

                                <td><asp:Label ID="lblData" runat="server" style="font-weight: 600;" Text='<%# DataBinder.Eval(Container.DataItem, "ped_dthrCriacao") %>' /></td>
                                <td><asp:Label ID="lblMesa" runat="server" Text='<%# "Mesa " + DataBinder.Eval(Container.DataItem, "mes_identificacao") %>' /></td>
                                <td><asp:Label ID="lblPedido" runat="server" Text='<%# "Pedido " + DataBinder.Eval(Container.DataItem, "mes_id") %>' /></td>
                                <td><asp:Label ID="lblValor" runat="server" Text='<%# String.Format("R${0:0.00}", DataBinder.Eval(Container.DataItem, "ped_valor")) %>' /></td>
                                <td>
                                    <asp:LinkButton runat="server" UseSubmitBehavior="False" CssClass="btn btn-outline-danger col-12" ID="btnExcluir" OnClick="btn_cancelar"> × </asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" UseSubmitBehavior="False" CssClass="btn btn-primary col-12" ID="btnConcluir" OnClick="btn_concluir"> Concluir </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>

    <!-- Modal Adicionar pagamento não agendado -->
    <div class="modal fade" id="mdlAdicionarPagamento" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">
                        <label>Adicionar pagamento</label>
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label>Qual a origem do pagamento</label>
                    <asp:TextBox runat="server" type="text" MaxLength="254" required="true" autocomplete="off" ID="txtOrigemAdicionar" CssClass="form-control"></asp:TextBox>
                    <small>exemplo: Comprar um doce, gorjeta, devolução</small><br />
                    <label class="mt-2">Insira o valor</label>
                    <asp:TextBox runat="server" type="number" min="-9999" max="9999" required="true" Text="0" autocomplete="off" ID="txtValorAdicionar" CssClass="form-control"></asp:TextBox>
                    <hr />
                    <asp:CheckBox runat="server" ID="ckbGorjeta" CssClass="w-100" Text="&nbsp; Gorjeta"></asp:CheckBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:Button runat="server" id="btnConfirmarPagamentoNaoAgendado" CssClass="btn btn-primary" Text="Adicionar" type="submit" OnClick="btnConfirmarPagamentoNaoAgendado_Click"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal concluir -->
    <div class="modal fade" id="mdlConcluir" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    Concluir pagamento
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label runat="server" ID="lblMesaPedidoConcluir"/>
                    <div class="my-2"></div>
                    Preço total: 
                    <asp:Label runat="server" ID="lblValorConcluir" CssClass="text-warning"/>
                    <div class="my-2"></div>

                    <asp:HiddenField runat="server" id="HidPedValorConcluir"/>
                    <asp:HiddenField runat="server" id="hidIdPedConcluir"/>
                    <asp:HiddenField runat="server" id="hidIdComConcluir"/>

                    <table id="tblPagamentoConcluir" class="display" style="width:100%">
                        <thead>
                            <tr>
                                <th>Item</th>
                                <th>Quantidade</th>
                                <th>Preço</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="rptPagamentoConcluir">
                                <ItemTemplate>
                                    <tr>
                                        <td runat="server" style='<%# Convert.ToInt32(Eval("pnp_disabled")) == 0 ? "color: white !important;" : "color: #ffc107 !important;" %>'><asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pro_nome") + (Convert.ToInt32(Eval("pnp_disabled")) == 0 ? "" : " (Cancelado)") %>'/></td>
                                        <td><asp:Label ID="lblQuantidade" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pnp_quantidade") %>' Visible='<%# Convert.ToInt32(Eval("pnp_disabled")) == 0 ? true : false %>'/></td>
                                        <td><asp:Label ID="lblpreco" runat="server" Text='<%# String.Format("R${0:0.00}", Convert.ToInt32(DataBinder.Eval(Container.DataItem, "pnp_quantidade")) * Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pro_valor"))) %>' Visible='<%# Convert.ToInt32(Eval("pnp_disabled")) == 0 ? true : false %>'/></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:LinkButton runat="server" CssClass="btn btn-success" ID="btnConfirmarConcluir" OnClick="btnConcluirPagamento_Click" Text="Concluir"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal excluir -->
    <div class="modal fade" id="mdlCancelar" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    Cancelar pagamento
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label runat="server" ID="lblMesaPedidoCancelar"/>
                    <div class="my-2"></div>
                    Preço total: 
                    <asp:Label runat="server" ID="lblValorCancelar" CssClass="text-warning"/>
                    <div class="my-2"></div>

                    <asp:HiddenField runat="server" id="hidIdPedCancelar"/>
                    <asp:HiddenField runat="server" id="hidIdComCancelar"/>

                    <table id="tblPagamentoCancelar" class="display" style="width:100%">
                        <thead>
                            <tr>
                                <th>Item</th>
                                <th>Quantidade</th>
                                <th>Preço</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="rptPagamentoCancelar">
                                <ItemTemplate>
                                    <tr>
                                        <td runat="server" style='<%# Convert.ToInt32(Eval("pnp_disabled")) == 0 ? "color: white !important;" : "color: #ffc107 !important;" %>'><asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pro_nome") + (Convert.ToInt32(Eval("pnp_disabled")) == 0 ? "" : " (Cancelado)") %>'/></td>
                                        <td><asp:Label ID="lblQuantidade" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pnp_quantidade") %>' Visible='<%# Convert.ToInt32(Eval("pnp_disabled")) == 0 ? true : false %>'/></td>
                                        <td><asp:Label ID="lblpreco" runat="server" Text='<%# String.Format("R${0:0.00}", Convert.ToInt32(DataBinder.Eval(Container.DataItem, "pnp_quantidade")) * Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pro_valor"))) %>' Visible='<%# Convert.ToInt32(Eval("pnp_disabled")) == 0 ? true : false %>'/></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:LinkButton runat="server" id="btnConfirmarCancelar" CssClass="btn btn-danger" Text="Cancelar" type="submit" OnClick="btnConfirmarCancelar_Click"/>
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
    
    if (<%= adicionar ? "true" : "false" %>)
    {
        $("#mdlAdicionarPagamento").modal('show');
    }

    if (<%= concluir ? "true" : "false" %>)
    {
        $("#mdlConcluir").modal('show');
    }

    if (<%= cancelar ? "true" : "false" %>)
    {
        $("#mdlCancelar").modal('show');
    }

    </script>
</asp:Content>