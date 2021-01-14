<%@ Page Title="Gerente" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="GerenciarMesas.aspx.cs" Inherits="Pages_gerente_GerenciarMesas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">
    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Gerenciamento de mesas</span>
        </div>
    </nav>
    <div class="pb-4"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <div class="row">
        <div class="col-12 col-sm-6 col-md-6 col-lg-4 py-1">
            <asp:LinkButton runat="server" class="btn btn-primary btn-block" onclick="btn_adicionar"> <i class="fa fa-plus-circle" aria-hidden="true"></i> Adicionar </asp:LinkButton>
        </div>
        <div class="col-12 col-sm-6 col-md-6 col-lg-8 py-1">
            <asp:Label runat="server" CssClass="" ID="res"  Text=""></asp:Label>
        </div>
    </div>
    
    <div class="row mt-3">

        <div runat="server" class="col-12 text-center my-5" id="divNenhumaMesa">
            <div class="h3 my-5"> Nenhuma mesa registrada </div>
        </div>

        <asp:Repeater runat="server" ID="rptMesa">
            <ItemTemplate>
                <div class="col-12 col-md-4 mb-3">
                    <div class="card bg-primary px-1 mx-0">
                        <div class="card-body">
                            <label> Identificação: </label>
                            <asp:Label ID="lblIdentificacao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mes_identificacao") %>' />
                            <%-- Guarda em campos hidden as informações de ID da mesa (para usar no CodeBehind) --%>
                            <asp:HiddenField runat="server" ID="hidId" Value='<%# DataBinder.Eval(Container.DataItem, "mes_id") %>' />
                            <hr />
                            <div class="row">
                                <asp:LinkButton runat="server" UseSubmitBehavior="False" CssClass="btn btn-primary mb-2 col-5 col-md-12" ID="btnEditar" OnClick="btn_editar"> <i class="fa fa-pencil-square-o" aria-hidden="true"></i> Editar </asp:LinkButton>
                                <asp:LinkButton runat="server" UseSubmitBehavior="False" CssClass="btn btn-outline-danger  mb-2 col-5 col-md-12 offset-2 offset-md-0" ID="btnExcluir" OnClick="btn_excluir"><i class="fa fa-times" aria-hidden="true"></i> Excluir </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <!-- Modal Adicionar -->
    <div class="modal fade" id="mdlAdicionar" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <label>Adicionar mesa</label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label>Insira a identificação da mesa</label>
                    <asp:TextBox runat="server" type="text" MaxLength="254" autocomplete="off" ID="txtIdentAdicionar" CssClass="form-control"></asp:TextBox>
                    <asp:HiddenField runat="server" id="hidIdAdicionar" Value=""/>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:Button runat="server" id="btnAdicionar" CssClass="btn btn-success" Text="Adicionar" type="submit" OnClick="btnAdicionar_Click"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal editar -->
    <div class="modal fade" id="mdlEditar" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <label>Editar mesa</label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label>Insira a identificação da mesa</label>
                    <asp:TextBox runat="server" type="text" MaxLength="254" autocomplete="off" ID="txtIdentEditar" CssClass="form-control"></asp:TextBox>
                    <asp:HiddenField runat="server" id="hidIdEditar" Value=""/>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:Button runat="server" id="btnConfirmarEditar" CssClass="btn btn-success" Text="Editar" type="submit" OnClick="btnConfirmarEditar_Click"/>
                </div>
            </div>
        </div>
    </div>


    <!-- Modal Remover -->
    <div class="modal fade" id="mdlRemover" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id=""> Remover mesa </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label>Remover</label>
                    <asp:HiddenField runat="server" id="hidIdmodalRemover" Value=""/>
                    <asp:Label runat="server" type="text" CssClass="text-warning" ID="lblMesaRemover"></asp:Label>?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"> Fechar </button>
                    <asp:Button runat="server" id="btnRemover" CssClass="btn btn-danger" Text="Remover" type="submit" OnClick="btnRemover_Click"/>
                </div>
            </div>
        </div>
    </div>

    <script>

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