<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<StudyStudio.Web.Models.Exercise.BrowseModel>" %>
<%@ Import Namespace="StudyStudio.Infrastructure.Queries" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Browse exercises</title>
</head>
<body>
    <%using(Html.BeginForm("Create", "Assignment")){%>
    <table>
    
        <%foreach (var exercise in Model.SearchResults)
        {%>
            <tr>
                <td><input type="checkbox" name="exerciseIds" value="<%=exercise.__document_id%>"/></td>
                <td><%=exercise.Body %></td>
            </tr>
        <%}%>
    </table>
    <input type="submit" value="Create Assignment">
    <%}%>
</body>
</html>
