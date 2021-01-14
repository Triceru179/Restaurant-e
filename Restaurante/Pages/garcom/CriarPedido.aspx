<%@ Page Title="Garçom" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="CriarPedido.aspx.cs" Inherits="Pages_garcom_CriarPedido" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">
    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Mesa <asp:Label runat="server" ID="lblMesa" Text=""/> </span>
        </div>
    </nav>
    <div class="pb-4"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <label runat="server" id="aaaaaaaa" style="color: black" visible="false">aaaaaaaa</label>
    <div class="row">
        <%-- Botão "adicionar", próximo ao topo da página (comanda fechada) --%>
        <div runat="server" id="divIniciar" class="col-12 col-sm-6 col-md-6 col-lg-4 py-1">
            <asp:LinkButton runat="server" ID="btnIniciar" CssClass="btn btn-primary btn-block" OnClick="btnIniciar_Click"> <i class="fa fa-plus-circle" aria-hidden="true"></i> Iniciar comanda </asp:LinkButton>
        </div>
        <%-- Botão "finalizar", próximo ao topo da página (comanda aberta) --%>
        <div runat="server" id="divFinalizar" visible="false" class="col-12 col-sm-6 col-md-6 col-lg-4 py-1">
            <asp:LinkButton runat="server" Visible="false" ID="btnFinalizar" CssClass="btn btn-success btn-block" OnClick="btn_finalizar"> <i class="fa fa-times" aria-hidden="true"></i> Finalizar comanda </asp:LinkButton>
        </div>
        <%-- Botão "excluir", próximo ao topo da página (comanda aberta) --%>
        <div runat="server" id="divExcluir" visible="false" class="col-12 col-sm-6 col-md-6 col-lg-4 py-1">
            <asp:LinkButton runat="server" Visible="false" ID="btnExcluir" CssClass="btn btn-outline-danger btn-block" OnClick="btn_excluir"> <i class="fa fa-times" aria-hidden="true"></i> Excluir comanda </asp:LinkButton>
        </div>
        <div class="col-12 col-sm-6 col-md-6 col-lg-4 py-1">
            <asp:Label runat="server" ID="res"></asp:Label>
        </div>
    </div>
    <div runat="server" class="row" id="divAddPedido">
        <%-- Botão "adicionar pedido" (comanda aberta) --%>
        <div class="col-12 col-sm-6 col-md-6 col-lg-4 py-1">
            <asp:LinkButton runat="server" ID="btnAddPedido" CssClass="btn btn-primary btn-block" OnClick="btnAddPedido_Click"> <i class="fa fa-plus-circle" aria-hidden="true"></i> Adicionar pedido </asp:LinkButton>
        </div>
    </div>

    <div runat="server" class="row my-5" id="divEsperar">
        <div class="col-12 text-center my-5">
            <div class="h3"> Aguardando uma nova comanda </div>
        </div>
    </div>

    <%-- Div com os pedidos --%>
    <div runat="server" class="row" id="divPedido">
        <div class="col-12">
            <%-- %%%%%%%%%%%%%%%%% inicio repeater pedidos %%%%%%%%%%%%%%%%%%% --%>
            <asp:Repeater ID="rptPedido" runat="server" OnItemDataBound="rptPedido_ItemDataBound">
                <ItemTemplate>
                    <div class="card bg-primary mb-2">
                        <div class="card-header">
                            Pedido <asp:Label runat="server" ID="lblNumPedido" Text='<%# Container.ItemIndex + 1 %>'/>
                            <div class="mt-2 row">

                                <asp:HiddenField runat="server" ID="hidIdPedido" Value='<%# DataBinder.Eval(Container.DataItem, "ped_id")%>' />
                                <asp:HiddenField runat="server" ID="hidValorTotal" Value='<%# DataBinder.Eval(Container.DataItem, "ped_valor")%>'/>

                                <%-- Botão "Editar pedido" --%>
                                <div runat="server" class="col-12 col-md-4" visible='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ped_solicitarPreparo")) == 0 ? true : false %>'>
                                    <asp:LinkButton runat="server" class="btn btn-block btn-primary" ID="btnEditarPedido" OnClick="btnEditarPedido_Click">Editar pedido</asp:LinkButton>
                                </div>
                                <%-- Botão "Solicitar preparo" do pedido --%>
                                <div runat="server" class="col-12 col-md-4 my-2 my-md-0"  visible='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ped_solicitarPreparo")) == 0 ? true : false %>'>
                                    <asp:LinkButton runat="server" class="btn btn-block btn-success" ID="btnSolicitarPreparo" OnClick="btn_solicitarPreparo">Solicitar preparo</asp:LinkButton>
                                </div>
                                <%-- Botão "Excluir pedido" --%>
                                <div runat="server" class="col-12 col-md-4"  visible='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ped_solicitarPreparo")) == 0 ? true : false %>'>
                                    <asp:LinkButton runat="server" class="btn btn-block btn-outline-danger" ID="btnCancelarPedido" OnClick="btn_excluirPedido">Cancelar pedido</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <table class="display" style="width:100%">
                                <thead>
                                    <tr>
                                        <th runat="server" visible='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ped_solicitarPreparo")) == 0 ? false : true %>'>Está pronto</th>
                                        <th>Produto</th>
                                        <th>Observação</th>
                                        <th>Quantidade</th>
                                        <th>Preço Total</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%-- %%%%%%%%%%%%%%%%% inicio repeater itens no pedido %%%%%%%%%%%%%%%%%%% --%>
                                    <asp:Repeater runat="server" ID="rptItemPedido">
                                        <ItemTemplate>
                                            <tr>
                                                <td runat="server" visible='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ped_solicitarPreparo")) == 0 ? false : true %>'><asp:Label ID="lblPnp_foiFeito" runat="server" Text='<%# Convert.ToInt32(Eval("pnp_disabled")) == 0 ? (DataBinder.Eval(Container.DataItem, "pnp_foiFeito").ToString().Equals("True") ? "Sim" : "Não") : "Cancelado" %>' CssClass=' <%#Convert.ToInt32(Eval("pnp_disabled")) == 0 ? "text-white" : "text-warning"%>'/></td>
                                                <td><asp:Label ID="lblPro_nome" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pro_nome") %>' /></td>
                                                <td><asp:Label ID="lblPnp_observacao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pnp_observacao") %>' Visible='<%# Convert.ToInt32(Eval("pnp_disabled")) == 0 ? true : false %>'/></td>
                                                <td><asp:Label ID="lblpnp_quantidade" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pnp_quantidade") %>' Visible='<%# Convert.ToInt32(Eval("pnp_disabled")) == 0 ? true : false %>'/></td>
                                                <td><asp:Label ID="lblpnp_valor" runat="server" Text='<%# String.Format("R${0:0.00}", Convert.ToDouble(Eval("pnp_valor")) * Convert.ToInt32(Eval("pnp_quantidade"))) %>' Visible='<%# Convert.ToInt32(Eval("pnp_disabled")) == 0 ? true : false %>'/></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <%-- %%%%%%%%%%%%%%%%% fim repeater itens no pedido %%%%%%%%%%%%%%%%%%% --%>
                                </tbody>
                            </table>
                        </div>
                        <div class="card-footer">
                            <span class="card-text"> Preço total do pedido: </span>
                            <asp:Label runat="server" ID="txtValorTotal" style="color: #e87a85;" Text="R$0,00"></asp:Label>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <%-- %%%%%%%%%%%%%%%%% fim repeater pedidos %%%%%%%%%%%%%%%%%%% --%>
        </div>
    </div>
    
    <!-- Modal finalizar comanda -->
    <div class="modal fade" id="mdlFinalizar" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id=""> Finalizar comanda </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label>Finalizar a comanda na mesa </label>
                    <asp:HiddenField runat="server" id="hidIdmodalFinalizar" Value=""/>
                    <asp:HiddenField runat="server" id="hidValorTotalFinalizar" Value=""/>

                    <asp:Label runat="server" type="text" CssClass="text-warning" ID="lblComandaFinalizar"></asp:Label>?
                    <br />
                    <label> Finalizar a comanda impedirá de adicionar novos pedidos ou editar pedidos existentes nesta comanda </label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:Button runat="server" id="Button1" CssClass="btn btn-success" Text="Finalizar" type="submit" OnClick="btnFinalizar_Click"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal excluir comanda -->
    <div class="modal fade" id="mdlExcluir" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id=""> Excluir comanda </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label>Excluir a comanda na mesa </label>
                    <asp:HiddenField runat="server" id="hidIdmodalExcluir" Value=""/>
                    <asp:Label runat="server" type="text" CssClass="text-warning" ID="lblComandaExcluir"></asp:Label>?

                    <label>Digite o motivo para o excluir a comanda</label>
                    <asp:TextBox runat="server" type="text" MaxLength="254" autocomplete="off" ID="txtMotivoComanda" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:Button runat="server" id="Button2" CssClass="btn btn-danger" Text="Excluir" type="submit" OnClick="btnExcluir_Click"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal solicitar preparo -->
    <div class="modal fade" id="mdlSolicitarPreparo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title"> Solicitar preparo</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label>
                        Solicitar o preparo do pedido <asp:Label runat="server" type="text" CssClass="text-warning" ID="lblSolicitarPreparo"></asp:Label>
                        na mesa <asp:Label runat="server" type="text" CssClass="text-warning" ID="lblComandaSolicitarPreparo"></asp:Label>?
                    </label>
                    <asp:HiddenField runat="server" id="hidIdmodalSolicitarPreparo" Value=""/>
                    <br />
                    <label> Solicitar o preparo do pedido impedirá a edição de seus itens ou a adição de novos itens a este pedido </label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:Button runat="server" id="Button3" CssClass="btn btn-success" Text="Solicitar" type="submit" OnClick="btnSolicitarPreparo_Click"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal excluir pedido -->
    <div class="modal fade" id="mdlExcluirPedido" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title"> Excluir pedido </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label>
                        Excluir o pedido <asp:Label runat="server" type="text" CssClass="text-warning" ID="lblPedido"></asp:Label>
                        na mesa <asp:Label runat="server" type="text" CssClass="text-warning" ID="lblComandaExcluirPedido"></asp:Label>?
                    </label>
                    <asp:HiddenField runat="server" id="hidIdmodalExcluirPedido" Value=""/>
                    
                    <label>Digite o motivo para o cancelamento</label>
                    <asp:TextBox runat="server" type="text" MaxLength="254" autocomplete="off" ID="txtMotivoPedido" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:Button runat="server" id="btnExcluirPedido" CssClass="btn btn-danger" Text="Excluir" type="submit" OnClick="btnExcluirPedido_Click"/>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            $('table.display').DataTable({
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

        if (<%= solicitarPreparo ? "true" : "false" %>)
        {
            $("#mdlSolicitarPreparo").modal('show');
        }

        if (<%= excluirPedido ? "true" : "false" %>)
        {
            $("#mdlExcluirPedido").modal('show');
        }
    </script>
</asp:Content>