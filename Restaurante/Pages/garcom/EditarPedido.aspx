<%@ Page Title="Garçom" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="EditarPedido.aspx.cs" Inherits="Pages_garcom_EditarPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">
    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand">
                Mesa <asp:Label runat="server" ID="lblMesa" Text=""/> - 
                Pedido <asp:Label runat="server" ID="lblPedido" Text=""/>
            </span>
        </div>
    </nav>
    <div class="pb-4"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <div class="row">
        <div class="col-12 col-sm-6 col-md-4 py-1">
            <asp:LinkButton runat="server" CssClass="btn btn-block btn-primary" ID="btnVoltar" OnClick="btnVoltar_Click" Text="Retornar a comanda"/>
        </div>
        <div class="col-12 col-sm-6 col-md-8 py-1">
            <asp:Label runat="server" ID="res"></asp:Label>
        </div>
    </div>
    <%-- ~~~~~~~~~~~~~~~~~~~~ Lista de produtos no pedido ~~~~~~~~~~~~~~~~~~~~ --%>
    <div class="row">
        <div class="col-12">
             <div class="card bg-primary mb-3">
                <div class="card-header">
                    <label class="">
                        Itens no pedido
                    </label>
                </div>
                <div class="card-body">
                    <table class="display" style="width:100%">
                        <thead>
                            <tr>
                                <th>Produto</th>
                                <th>Observação</th>
                                <th>Quantidade</th>
                                <th>Preço Total</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <%-- %%%%%%%%%%%%%%%%% inicio repeater itens no pedido %%%%%%%%%%%%%%%%%%% --%>
                            <asp:Repeater runat="server" ID="rptProdutosNoPedido">
                                <ItemTemplate>
                                    <tr>
                                        <asp:HiddenField runat="server" id="hidIdProdutosNoPedido" Value='<%# DataBinder.Eval(Container.DataItem, "pnp_id") %>'/>
                                        <td><asp:Label ID="lblPro_nome" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pro_nome") %>' /></td>
                                        <td><asp:Label ID="lblPnp_observacao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pnp_observacao") %>' /></td>
                                        <td><asp:Label ID="lblpnp_quantidade" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pnp_quantidade") %>' /></td>
                                        <td><asp:Label ID="lblpnp_valor" runat="server" Text='<%# String.Format("R${0:0.00}", Convert.ToDouble(Eval("pnp_valor")) * Convert.ToInt32(Eval("pnp_quantidade"))) %>' /></td>
                                        <td>
                                            <asp:LinkButton runat="server" CssClass="btn btn-danger" Text="×" id="btnRemoverProduto" OnClick="btnRemoverProduto_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <%-- %%%%%%%%%%%%%%%%% fim repeater itens no pedido %%%%%%%%%%%%%%%%%%% --%>
                        </tbody>
                    </table>
                </div>
                <div class="card-footer">
                    <span class="card-text"> Preço total do pedido: </span>
                    <asp:Label runat="server" ID="txtValorTotal" Enabled="false" style="color: #e87a85;" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <%-- ~~~~~~~~~~~~~~~~~~~~ Cardápio ~~~~~~~~~~~~~~~~~~~~ --%>
    <div class="row">
        <div class="col-12">
             <div class="card bg-primary mb-2">
                    <div class="card-header">
                        <label class="">
                            Cardápio
                        </label>
                    </div>
                <div class="card-body">
                    <table class="display tbl" style="width:100%">
                        <thead>
                            <tr>
                                <th>Produto</th>
                                <th>Descrição</th>
                                <th>Preço</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="rptCardapio">
                                <ItemTemplate>
                                    <tr>
                                        <asp:HiddenField runat="server" id="hidIdProduto" Value='<%# DataBinder.Eval(Container.DataItem, "pro_id") %>'/>
                                        <asp:HiddenField runat="server" id="hidValorProduto" Value='<%# DataBinder.Eval(Container.DataItem, "pro_valor") %>'/>

                                        <td><asp:Label ID="lblpro_nome" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pro_nome") %>' /></td>
                                        <td><asp:Label ID="lblpro_descricao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pro_descricao") %>' /></td>
                                        <td><asp:Label ID="lblpro_valor" runat="server" Text='<%# String.Format("R${0:0.00}", Convert.ToDouble(Eval("pro_valor"))) %>' /></td>
                                        <td>
                                            <asp:LinkButton runat="server" CssClass="btn btn-success" Text="Adicionar" id="btnAdicionarProduto" OnClick="btn_adicionar"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                <div class="card-footer">
                    <span class="card-text"> Preço total do pedido: </span>
                    <asp:Label runat="server" ID="Label1" Enabled="false" style="color: #e87a85;" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal adicionar produto -->
    <div class="modal fade" id="mdlAdicionarProduto" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title"> Adicionar item </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField runat="server" id="hidProdutoIdmodalAdicionar" />
                    <asp:HiddenField runat="server" id="hidProdutoValorAdicionar" />

                    <span>Item</span><br />
                    <asp:TextBox runat="server" Enabled="false" CssClass="w-100" ID="txtPro_nome"/><br />
                    <span>Valor</span><br />
                    <asp:TextBox runat="server" Enabled="false" CssClass="w-100" ID="txtPro_valor"/>
                    <asp:Label runat="server" type="text" CssClass="text-warning" ID="lblComandaExcluirPedido"></asp:Label>
                    <label>Digite a quantidade do item</label>
                    <asp:TextBox runat="server" type="number" min="1" max="99" Text="1" required="true" autocomplete="off" ID="txtQuantidade" CssClass="form-control txtadd"/>

                    <label>Observação do produto</label>
                    <asp:TextBox runat="server" type="text" MaxLength="254" autocomplete="off" ID="txtObservacao" CssClass="form-control txtobs"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:Button runat="server" id="btnAdicionar" CssClass="btn btn-success" Text="Adicionar" type="submit" OnClick="btnAdicionarProduto_Click"/>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            $('.display').DataTable({
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

        $('#mdlAdicionarProduto .txtadd').val('1');
        $('#mdlAdicionarProduto .txtobs').val('');

        if (<%= adicionar ? "true" : "false" %>)
        {
            $("#mdlAdicionarProduto").modal('show');
        }
    </script>
</asp:Content>