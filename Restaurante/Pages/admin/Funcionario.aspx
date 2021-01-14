<%@ Page Title="Administrador" Language="C#" MasterPageFile="~/Pages/Page.Master" AutoEventWireup="true" CodeFile="funcionario.aspx.cs" Inherits="Pages_admin_Funcionario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" runat="server">

    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Gerenciar funcionário </span>
        </div>
    </nav>
    <div class="pb-4"></div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="row">
        <div class="col-12 col-sm-6 col-md-6 col-lg-4 py-1">
            <asp:LinkButton runat="server" class="btn btn-primary btn-block" onclick="btn_adicionar"> <i class="fa fa-user-plus" aria-hidden="true"></i> Adicionar </asp:LinkButton>
        </div>
        <div class="col-12 col-sm-6 col-md-6 col-lg-8 py-1">
            <asp:Label runat="server" CssClass="" ID="res"  Text=""/>
        </div>
    </div>

    <!-- Tabela com todos os funcionários -->
    <div class="card bg-primary">
        <div class="card-body">
            <table id="tblFuncionario" class="display" style="width:100%">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Nome</th>
                        <th>Telefone</th>
                        <th>E-mail</th>
                        <th>Cargo</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptFuncionario">
                        <ItemTemplate>
                            <tr>
                                <asp:HiddenField runat="server" ID="hidId" Value='<%# DataBinder.Eval(Container.DataItem, "fun_id") %>' />
                                <asp:HiddenField runat="server" id="hidPermissao" Value='<%# DataBinder.Eval(Container.DataItem, "fun_permissao") %>' />

                                <td><asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fun_id") %>' /></td>
                                <td><asp:Label ID="lblNome" runat="server" style="font-weight: 600;" Text='<%# DataBinder.Eval(Container.DataItem, "fun_nome") %>' /></td>
                                <td><asp:Label ID="lblTelefone" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fun_Telefone") %>' /></td>
                                <td><asp:Label ID="lblEmail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fun_email") %>' /></td>
                                <td>
                                    <div class="pb-1">
                                        <%# ((Convert.ToInt32(DataBinder.Eval(Container.DataItem, "fun_permissao")) & Permissoes.founder) != 0) ? "Fundador <br />" : "" %>
                                        <%# ((Convert.ToInt32(DataBinder.Eval(Container.DataItem, "fun_permissao")) & Permissoes.admin)   != 0) ? "Administrador <br />" : "" %>
                                        <%# ((Convert.ToInt32(DataBinder.Eval(Container.DataItem, "fun_permissao")) & Permissoes.gerente) != 0) ? "Gerente <br />" : "" %>
                                        <%# ((Convert.ToInt32(DataBinder.Eval(Container.DataItem, "fun_permissao")) & Permissoes.garcom)  != 0) ? "Garçom <br />" : "" %>
                                        <%# ((Convert.ToInt32(DataBinder.Eval(Container.DataItem, "fun_permissao")) & Permissoes.caixa)   != 0) ? "Caixa <br />" : "" %>
                                        <%# ((Convert.ToInt32(DataBinder.Eval(Container.DataItem, "fun_permissao")) & Permissoes.cozinha) != 0) ? "Cozinha <br />" : "" %>
                                    </div>
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" CssClass="btn btn-primary my-2 col-12" ID="btnEditar" OnClick="btn_editar"          Visible='<%# (Convert.ToInt32(Session["permissao"]) & Permissoes.founder) != 0 ? true : (Convert.ToInt32(DataBinder.Eval(Container.DataItem, "fun_permissao")) & Permissoes.founder) != 0 ? false : (Convert.ToInt32(DataBinder.Eval(Container.DataItem, "fun_permissao")) & Permissoes.admin) != 0 ? (Convert.ToInt32(Session["fun_id"]) == Convert.ToInt32(DataBinder.Eval(Container.DataItem, "fun_id"))) ? true : false : true %>'> Editar </asp:LinkButton>
                                    <asp:LinkButton runat="server" CssClass="btn btn-outline-danger mb-2 col-12" ID="btnExcluir" OnClick="btn_excluir" Visible='<%# (Convert.ToInt32(Session["permissao"]) & Permissoes.founder) != 0 ? (Convert.ToInt32(Session["permissao"]) == Convert.ToInt32(DataBinder.Eval(Container.DataItem, "fun_permissao"))) ? false : (Convert.ToInt32(Session["permissao"]) == Convert.ToInt32(DataBinder.Eval(Container.DataItem, "fun_permissao"))) ? false : true : (Convert.ToInt32(DataBinder.Eval(Container.DataItem, "fun_permissao")) & Permissoes.founder) != 0 ? false : (Convert.ToInt32(DataBinder.Eval(Container.DataItem, "fun_permissao")) & Permissoes.admin) != 0 ? false : true %>'> Excluir </asp:LinkButton>
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
                    <h5 class="modal-title">
                        <label>Adicionar funcionário</label>
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label>Insira o nome</label>
                    <asp:TextBox runat="server" type="text" MaxLength="254" autocomplete="off" ID="txtNomeAdicionar" CssClass="form-control"></asp:TextBox>
                    <label>Insira o telefone</label>
                    <asp:TextBox runat="server" type="text" MaxLength="254" autocomplete="off" ID="txtTelefoneAdicionar" CssClass="form-control"></asp:TextBox>
                    <label>Insira o email</label>
                    <asp:TextBox runat="server" type="email" MaxLength="254" autocomplete="off" ID="txtEmailAdicionar" CssClass="form-control"></asp:TextBox>
                    <label>Insira a senha</label>
                    <asp:TextBox runat="server" type="password" MaxLength="254" ID="txtSenhaAdicionar" CssClass="form-control"></asp:TextBox>
                    <label>Insira a senha novamente</label>
                    <asp:TextBox runat="server" type="password" MaxLength="254" ID="txtRepitaSenhaAdicionar" CssClass="form-control"></asp:TextBox>

                    <hr />
                    <label>Selecione os cargos do funcionário</label><br />
                    <% if (((Convert.ToInt32(Session["permissao"]) & Permissoes.founder) != 0)) { %>
                    <asp:CheckBox runat="server" ID="ckbAdminAdicionar" CssClass="w-100" Text="&nbsp; Administrador"></asp:CheckBox><br />
                    <% } %>
                    <asp:CheckBox runat="server" ID="ckbGerenAdicionar" CssClass="w-100" Text="&nbsp; Gerente"></asp:CheckBox><br />
                    <asp:CheckBox runat="server" ID="ckbGarcoAdicionar" CssClass="w-100" Text="&nbsp; Garçom"></asp:CheckBox><br />
                    <asp:CheckBox runat="server" ID="ckbCozinAdicionar" CssClass="w-100" Text="&nbsp; Cozinha"></asp:CheckBox><br />
                    <asp:CheckBox runat="server" ID="ckbCaixaAdicionar" CssClass="w-100" Text="&nbsp; Caixa"></asp:CheckBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:Button runat="server" id="btnAdicionar" CssClass="btn btn-primary" Text="Adicionar" type="submit" OnClick="btnAdicionar_Click"/>
                    
                </div>
            </div>
        </div>
    </div>

    <!-- Modal editar -->
    <div class="modal fade" id="mdlEditar" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">
                        <label>Editar funcionário</label>
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label>Insira o nome</label>
                    <asp:TextBox runat="server" type="text" MaxLength="254" autocomplete="off" ID="txtNomeEditar" CssClass="form-control"></asp:TextBox>
                    <label>Insira o telefone</label>
                    <asp:TextBox runat="server" type="text" MaxLength="254" autocomplete="off" ID="txtTelefoneEditar" CssClass="form-control"></asp:TextBox>
                    <label>Insira o email</label>
                    <asp:TextBox runat="server" type="email" MaxLength="254" autocomplete="off" ID="txtEmailEditar" CssClass="form-control"></asp:TextBox>
                    <label>Insira a senha</label>
                    <asp:TextBox runat="server" type="password" MaxLength="254" ID="txtSenhaEditar" CssClass="form-control"></asp:TextBox>
                    <label>Insira a senha novamente</label>
                    <asp:TextBox runat="server" type="password" MaxLength="254" ID="txtRepitaSenhaEditar" CssClass="form-control"></asp:TextBox>

                    <asp:HiddenField runat="server" id="hidIdEditar"/>
                    <asp:HiddenField runat="server" id="hidPermissaoEditar"/>

                    <hr />
                    <label>Selecione os cargos do funcionário</label><br />
                    <asp:CheckBox runat="server" ID="ckbFundaEditar" Enabled="false" Visible="false" CssClass="w-100" Text="&nbsp; Fundador"></asp:CheckBox><br />
                    <% if (((Convert.ToInt32(Session["permissao"]) & Permissoes.founder) != 0) || ((Convert.ToInt32(Session["permissao"]) & Permissoes.admin) != 0)) { %>
                    <asp:CheckBox runat="server" ID="ckbAdminEditar" CssClass="w-100" Text="&nbsp; Administrador"></asp:CheckBox><br />
                    <% } %>
                    <asp:CheckBox runat="server" ID="ckbGerenEditar" CssClass="w-100" Text="&nbsp; Gerente"></asp:CheckBox><br />
                    <asp:CheckBox runat="server" ID="ckbGarcoEditar" CssClass="w-100" Text="&nbsp; Garçom"></asp:CheckBox><br />
                    <asp:CheckBox runat="server" ID="ckbCozinEditar" CssClass="w-100" Text="&nbsp; Cozinha"></asp:CheckBox><br />
                    <asp:CheckBox runat="server" ID="ckbCaixaEditar" CssClass="w-100" Text="&nbsp; Caixa"></asp:CheckBox>
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
                    <h5 class="modal-title" id=""> Remover funcionário </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label>Remover</label>
                    <asp:HiddenField runat="server" id="hidIdmodalRemover" Value=""/>
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
            $('#tblFuncionario').DataTable({
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