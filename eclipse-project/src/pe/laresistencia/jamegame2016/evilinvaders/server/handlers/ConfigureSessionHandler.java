package pe.laresistencia.jamegame2016.evilinvaders.server.handlers;

import java.io.IOException;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import pe.laresistencia.jamegame2016.evilinvaders.server.model.SessionManager;

@SuppressWarnings("serial")
public class ConfigureSessionHandler extends HttpServlet{
	
	@Override
	protected void doPost(HttpServletRequest request, HttpServletResponse response ) 
    {
		try {
			response.setContentType("text/plain");
			
			String session = request.getParameter("session");
			String cat = SessionManager.instance.configureSession(session);
			
			response.getWriter().write(cat);
			
		} catch (IOException e) {
			e.printStackTrace();
		}
    }
	
}
