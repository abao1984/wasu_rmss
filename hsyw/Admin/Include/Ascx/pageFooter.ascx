<%@ Control Language="C#" AutoEventWireup="true" CodeFile="pageFooter.ascx.cs" Inherits="pageFooter" %>
<!-- 页尾开始 -->
<br />
<br />
<br />

<a name="bottom"></a>
<div >
    <div style="text-align:center">
        <div id="footer">
	          <div class="footerNav">
	  		        <div class="footerFleft">
				        <div class="footerFleftCopy">
				            <% =Session["PageTitle"].ToString().Trim()%>
	  		                 &nbsp;- &nbsp;
	  		                <% =Session["ClientName"].ToString().Trim()%>
				                <br />
                            <% =Session["CopyRightCompany"].ToString().Trim()%>
                             &nbsp;- &nbsp;
                            <% =Session["CopyRightAuthor"].ToString().Trim()%>
                            
                            
				        </div>
        				
			        </div>
        			
			        <div class="footerFright">
			            <div class="footerGotop"><img src="Images/Footer/gototop2.gif" alt="返顶部" border="0" onclick="window.scrollTo(0,0)" style="cursor:pointer"/></div>
			        </div>

	          </div>
        </div>
    </div>
</div>
<!-- 页尾结束 -->
