<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeFile="ListNews.aspx.cs" Inherits="CenturyRayonSchool.ListNews" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        div1-style {
            background-color: #cccccc00; 
            font-family: Arial, Helvetica, sans-serif; 
            font-size: medium; 
            font-weight: normal; 
            font-style: normal;
            /*margin-left:100px;*/
            margin-top:50px;
            /*position:fixed;*/

        }


         .divmessage {
             width: 100%;
             background-color:navy;
              
              color:white;
             
              font-size:larger;
              padding-top:5px;
         }
       

      
        

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid dashboard-content" >
            <div class="row mb-2">
                <asp:Label ID="lblformtitle" Text="Notice List" runat="server" CssClass="font-weight-bolder " Style="color: navy; font-size: xx-large;" />
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="divmessage">
                        <asp:Label ID="lblmessage" Text="" runat="server" />
                    </div>

                </div>
                 <div class="col-md-12">
                       <div class="table-responsive">
                            <asp:GridView ID="newsgridview" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" OnRowCommand="newsgridview_RowCommand" DataKeyNames="filepath">
                                 <Columns >
                                     <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-Width="30px" />
                                     <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="30px" />
                                     <asp:BoundField DataField="topicname" HeaderText="Topic Name" ItemStyle-Width="30px"/>
                                     <asp:BoundField DataField="topicdescription" HeaderText="Topic Description" ItemStyle-Width="300px" />
                                     <asp:BoundField DataField="filename" HeaderText="File Name" ItemStyle-Width="30px" />
                                     <asp:BoundField DataField="filepath" HeaderText="File Path" ItemStyle-Width="30px"  Visible="false"/>
                                     <asp:TemplateField ItemStyle-Width="10px" HeaderText="Download">
                                         <ItemTemplate>
                                             <asp:Button ID="downloadbuttonlink" runat="server" Text="Download" visible='<%#setVisiblelabel(Eval("filepath").ToString())%>' CssClass="btn btn-success" CommandName="downloadfile" CommandArgument="<%# ((GridViewRow) Container).RowIndex%>"/>
                                             <%--<asp:HyperLink ID="downloadlink" runat="server" NavigateUrl='<%#Eval("filepath")%>' visible='<%#setVisiblelabel(Eval("filepath").ToString())%>'>Download</asp:HyperLink>--%>
                                             
                                         </ItemTemplate>

                                         <ItemStyle Width="30px"></ItemStyle>
                                     </asp:TemplateField>

                                     <asp:TemplateField ItemStyle-Width="30px" HeaderText="Action">
                                         <ItemTemplate>
                                             <%--<asp:Button ID="btnremove" Text="Remove" runat="server" CssClass="btn btn-danger" CommandName="removeImage" CommandArgument="<%# ((GridViewRow) Container).RowIndex%>" />--%>
                                              <div style="width:100%;">
                                                     <asp:Button ID="btneditnews" Text="Edit" runat="server" CssClass="btn btn-secondary" CommandName="editnews" CommandArgument="<%# ((GridViewRow) Container).RowIndex%>" visible='<%# setButtonVisibility(Convert.ToDateTime(Eval("createddate")).ToString("yyyy/MM/dd").Replace("-","/")) %>' style="margin-bottom:2px; width:100%;"/>
                                                     <asp:Button ID="btndelete" Text="Remove" runat="server" CssClass="btn btn-danger" CommandName="deletenews" CommandArgument="<%# ((GridViewRow) Container).RowIndex%>"  visible='<%# setButtonVisibility(Convert.ToDateTime(Eval("createddate")).ToString("yyyy/MM/dd").Replace("-","/")) %>' style="width:100%;" OnClientClick="Confirm()"/>
                                              </div>
                                         </ItemTemplate>

                                         <ItemStyle Width="30px"></ItemStyle>
                                     </asp:TemplateField>

                                 </Columns>
                                <HeaderStyle BackColor="Black" ForeColor="White" />
                            </asp:GridView>
                       </div>
                 </div>
            </div>
        </div>
  


    <script>



        $(document).ready(function () {

            $("#menu_studentcorner").addClass("show");
            $("#item_studentcorner").addClass("active");
            $("#menu_viewnews").addClass("active");
            $("#menu_viewnews").css("background-color", "white");
            $("#menu_viewnews").css("color", "black");

        });



        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you really want to remove record?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
