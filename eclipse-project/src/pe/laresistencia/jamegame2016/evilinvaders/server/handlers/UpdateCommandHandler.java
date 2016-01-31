package pe.laresistencia.jamegame2016.evilinvaders.server.handlers;

import java.io.IOException;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import pe.laresistencia.jamegame2016.evilinvaders.server.model.CatActor;
import pe.laresistencia.jamegame2016.evilinvaders.server.model.SessionManager;

@SuppressWarnings("serial")
public class UpdateCommandHandler extends HttpServlet{
	
	@Override
	protected void doPost(HttpServletRequest request, HttpServletResponse response ) 
    {
		try {
			response.setContentType("text/plain");
			
			String session = request.getParameter("session");
			float x = Float.parseFloat(request.getParameter("x"));
			float y = Float.parseFloat(request.getParameter("y"));
			
			CatActor cat = SessionManager.instance.getCatActorFromSession(session);
			
			cat.x = x;
			cat.y = y;
						
			CatActor otherCat = SessionManager.instance.getOtherCatActorFromSession(session);
			
			if(otherCat == null)
			{
				response.getWriter().write("NIL");
				return;
			}
			
			if(SessionManager.instance.matchEnded)
			{
				if(cat.winner)
				{
					response.getWriter().write("WIN");
				}
				else
				{
					response.getWriter().write("LOSE");
				}
				return;
			}
			
			response.getWriter().write("" + cat.x + ";" + cat.y + ";" + otherCat.x + ";" + otherCat.y);
			
		} catch (IOException e) {
			e.printStackTrace();
		}
    }
	
}
