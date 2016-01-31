package pe.laresistencia.jamegame2016.evilinvaders.server.handlers;

import java.io.IOException;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

@SuppressWarnings("serial")
public class PingHandler extends HttpServlet{
	
	@Override
	protected void doPost(HttpServletRequest request, HttpServletResponse response ) 
    {
		try {
			response.setContentType("text/plain");
			response.getWriter().println("ping");
		} catch (IOException e) {
			e.printStackTrace();
		}
    }
	
}
