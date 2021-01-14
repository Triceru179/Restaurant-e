<%@ Page Title="Gerente" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="Cardapio.aspx.cs" Inherits="Pages_gerente_Cardapio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">

    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Gerenciar cardápio </span>
        </div>
    </nav>
    <div class="pb-4"></div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <div class="row">
        <div class="col-12 col-sm-6 col-md-6 col-lg-4 py-1">
            <asp:LinkButton runat="server" class="btn btn-primary btn-block" ID="btnAdicionar" onclick="btn_adicionar"> <i class="fa fa-plus-circle" aria-hidden="true"></i> Adicionar </asp:LinkButton>
        </div>
        <div class="col-12 col-sm-6 col-md-6 col-lg-8 py-1">
            <asp:Label runat="server" CssClass="" ID="res"  Text=""></asp:Label>
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
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptProduto">
                        <ItemTemplate>
                            <tr>
                                <asp:HiddenField runat="server" ID="hidId" Value='<%# DataBinder.Eval(Container.DataItem, "pro_id") %>'/>
                                <asp:HiddenField runat="server" ID="hidComplemento" Value='<%# DataBinder.Eval(Container.DataItem, "pro_complemento") %>'/>
                                <asp:HiddenField runat="server" ID="hidDisponivel" Value='<%# DataBinder.Eval(Container.DataItem, "pro_disponivel") %>'/>
                                <asp:HiddenField runat="server" ID="hidValor" Value='<%# DataBinder.Eval(Container.DataItem, "pro_valor") %>'/>

                                <td><asp:Label ID="lblNome" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pro_nome") %>' /></td>
                                <td><asp:Label runat="server" ID="lblDescricao" Text='<%# DataBinder.Eval(Container.DataItem, "pro_descricao") %>'></asp:Label></td>
                                <td><asp:Label runat="server" ID="lblValor" Text='<%# String.Format("R$ {0:0.00}", Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pro_valor"))) %>'/></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "pro_complemento").ToString().Equals("True") ? "Sim" : "Não" %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "pro_disponivel").ToString().Equals("True") ? "Sim" : "Não" %> </td>
                                <td>
                                    <asp:Button runat="server" UseSubmitBehavior="False" CssClass="btn btn-primary col-12 my-2" ID="btnEditar" Text="Editar" OnClick="btn_editar"/>
                                    <asp:Button runat="server" UseSubmitBehavior="False" CssClass="btn btn-outline-danger col-12 mb-2" ID="btnExcluir" Text="Excluir" OnClick="btn_excluir"/>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>

    <!-- Modal Adicionar -->
    <div class="modal fade" id="mdlAdicionar" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle"> 
                        <label>Adicionar produto</label>
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField runat="server" id="hidIdAdicionar" Value=""/>

                    <label>Insira o nome</label>
                    <asp:TextBox runat="server" type="text" MaxLength="254" autocomplete="off" ID="txtNomeAdicionar" CssClass="form-control"/>
                    <label>Insira a descrição</label>
                    <asp:TextBox runat="server" type="text" MaxLength="254" autocomplete="off" ID="txtDescricaoAdicionar" CssClass="form-control"/>
                    <label>Insira o valor</label>
                    <asp:TextBox runat="server" type="text" min="0" max="9999" autocomplete="off" ID="txtValorAdicionar" CssClass="form-control"/>
                    <hr />
                    <asp:CheckBox runat="server" ID="ckbAdicionarComplemento" Text="&nbsp; É complemento"/>
                    <br />
                    <asp:CheckBox runat="server" ID="ckbAdicionarDisponivel" Text="&nbsp; Está disponível"/>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:Button runat="server" id="btnAdicionarConfirmar" CssClass="btn btn-primary" Text="Adicionar" type="submit" OnClick="btnAdicionar_Click"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Editar -->
    <div class="modal fade" id="mdlEditar" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">
                        <label>Editar produto</label>
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField runat="server" id="hidIdEditar" Value=""/>

                    <label>Insira o nome</label>
                    <asp:TextBox runat="server" type="text" MaxLength="254" autocomplete="off" ID="txtNomeEditar" CssClass="form-control"/>
                    <label>Insira a descrição</label>
                    <asp:TextBox runat="server" type="text" MaxLength="254" autocomplete="off" ID="txtDescricaoEditar" CssClass="form-control"/>
                    <label>Insira o valor</label>
                    <asp:TextBox runat="server" type="text" min="0" max="9999" autocomplete="off" ID="txtValorEditar" CssClass="form-control"/>
                    <hr />
                    <asp:CheckBox runat="server" ID="ckbEditarComplemento" Text="&nbsp; É complemento"/>
                    <br />
                    <asp:CheckBox runat="server" ID="ckbEditarDisponivel" Text="&nbsp; Está disponível"/>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:Button runat="server" id="btnConfirmarEditar" CssClass="btn btn-success" Text="Salvar" type="submit" OnClick="btnConfirmarEditar_Click"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Remover -->
    <div class="modal fade" id="mdlRemover" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id=""> Remover produto </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label>Remover</label>
                    <asp:HiddenField runat="server" id="hidIdRemover" Value=""/>
                    <asp:Label runat="server" type="text" CssClass="text-warning" ID="lblNomeRemover"></asp:Label>?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:Button runat="server" id="btnRemover" CssClass="btn btn-danger" Text="Remover" type="submit" OnClick="btnRemover_Click"/>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            $('#tblProdutos').DataTable({
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
            $("#mdlAdicionar").modal('show');
        }

        if (<%= editar ? "true" : "false" %>)
        {
            $("#mdlEditar").modal('show');
        }

        if (<%= remover ? "true" : "false" %>)
        {
            $("#mdlRemover").modal('show');
        }
    </script>
</asp:Content>