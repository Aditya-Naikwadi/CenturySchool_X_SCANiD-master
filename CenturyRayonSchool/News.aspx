<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="CenturyRayonSchool.News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='https://fonts.googleapis.com/css2?family=Quintessential&display=swap' rel='stylesheet' />


    <style>
        .head1 {
            font-size: xxx-large;
            font-weight: 600;
            margin-top: 115px;
            color: #74cee4;
            font-family: Quintessential;
            text-align: center;
        }
        .pd {
            padding:10px;
           
          
        }

        .btn {
    display: inline-block;
    font-weight: 400;
    color: #212529;
    text-align: center;
    vertical-align: middle;
    -webkit-user-select: none;
    -moz-user-select: none;
    -ms-user-select: none;
    user-select: none;
    background-color: transparent;
    border: 1px solid transparent;
    padding: 0.375rem 0.75rem;
    font-size: 1rem;
    line-height: 1.5;
    border-radius: 0.25rem;
    transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;
}

        .btn:not(:disabled):not(.disabled) {
    cursor: pointer;
}

        .btn-success {
    color: #fff;
    background-color: #28a745;
    border-color: #28a745;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="nicdark_section" style="background-color:#9d95951a">
        <header id="header" class="header">
           
            <div class="head1">Latest Notices</div>
            <br />
        </header>
      
                    
           
               
                   
                    <div class="nicdark_section">
                       
                        <div class="notices" style="width:80%;margin:auto;">
                             <asp:GridView ID="newsgridview" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" OnRowCommand="newsgridview_RowCommand" DataKeyNames="filepath" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-Width="50px" Visible="false"/>
                                    <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="90px" ItemStyle-CssClass="pd"/>
                                    <asp:BoundField DataField="topicname" HeaderText="Topic Name" ItemStyle-Width="170px" ItemStyle-CssClass="pd"/>
                                    <asp:BoundField DataField="topicdescription" HeaderText="Topic Description" ItemStyle-Width="600px" ItemStyle-CssClass="pd" />
                                     <asp:BoundField DataField="filepath" HeaderText="File Path" ItemStyle-Width="10px"  Visible="false"/>
                                    

                                    <asp:TemplateField  HeaderText="View" ItemStyle-CssClass="pd" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <%--<asp:Button ID="downloadbuttonlink" runat="server" Text="Download" visible='<%#setVisiblelabel(Eval("filepath").ToString())%>' CssClass="btn btn-success" CommandName="downloadfile" CommandArgument="<%# ((GridViewRow) Container).RowIndex%>"/>--%>
                                           
                                           <asp:HyperLink ID="downloadlink" runat="server" NavigateUrl='<%#Eval("filepath")%>' Target="_blank"  CssClass="btn btn-success" visible='<%#setVisiblelabel(Eval("filepath").ToString())%>'>View</asp:HyperLink>
                                        </ItemTemplate>

                                      
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle BackColor="Black" ForeColor="White" />
                            </asp:GridView>
                        </div>
                           
                       
                    </div>
                    <div class="nicdark_space20"></div>
               
            
  
           

    </section>
</asp:Content>
