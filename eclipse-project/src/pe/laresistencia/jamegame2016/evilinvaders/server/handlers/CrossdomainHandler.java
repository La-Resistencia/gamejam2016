package pe.laresistencia.jamegame2016.evilinvaders.server.handlers;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

@SuppressWarnings("serial")
public class CrossdomainHandler extends HttpServlet {

	@Override
	protected void doGet( HttpServletRequest request, HttpServletResponse response ) throws ServletException, IOException
    {
		response.setContentType("application/xml; charset=utf-8");
        response.setStatus(HttpServletResponse.SC_OK);

        response.getWriter().println("<cross-domain-policy>");
        response.getWriter().println("  <site-control permitted-cross-domain-policies=\"master-only\"/>");
        response.getWriter().println("  <allow-access-from domain=\"*\"/>");
        response.getWriter().println("  <allow-http-request-headers-from domain=\"*\" headers=\"*\"/>");
        response.getWriter().println("</cross-domain-policy>");
	}

}
