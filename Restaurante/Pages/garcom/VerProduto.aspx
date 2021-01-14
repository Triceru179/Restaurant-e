<%@ Page Title="Garçom" Language="C#" MasterPageFile="~/Pages/Page.master" AutoEventWireup="true" CodeFile="VerProduto.aspx.cs" Inherits="Pages_garcom_VerProduto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" Runat="Server">

    <nav class="navbar nav-menu bg-secondary fixed-top p-0">
        <div class="container-fluid">
            <div class="pl-3"></div>
            <%-- adiciona o título da página ao menu superior, indicando a página atual --%>
            <span class="text-light navbar-brand"> Visualizar cardápio </span>
        </div>
    </nav>
    <div class="pb-4"></div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <div class="card bg-primary">
        <div class="card-body">
            <table id="tblProdutos" class="display" style="width:100%">
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Descrição</th>
                        <th>Valor</th>
                        <th>É complemento?</th>
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
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
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
    </script>
</asp:Content>