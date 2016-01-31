package pe.laresistencia.jamegame2016.evilinvaders.server.handlers;

import java.io.IOException;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import pe.laresistencia.jamegame2016.evilinvaders.server.model.CatActor;
import pe.laresistencia.jamegame2016.evilinvaders.server.model.SessionManager;

@SuppressWarnings("serial")
public class CatchHandler extends HttpServlet{
	
	@Override
	protected void doPost(HttpServletRequest request, HttpServletResponse response ) 
    {
		try {
			response.setContentType("text/plain");
			
			String session = request.getParameter("session");
			
			CatActor cat = SessionManager.instance.getCatActorFromSession(session);
			CatActor otherCat = SessionManager.instance.getOtherCatActorFromSession(session);
			
			if(otherCat == null)
			{
				response.getWriter().write("NIL");
				return;
			}
			
			float deltaX = Math.abs(cat.x - otherCat.x);
			float deltaY = Math.abs(cat.y - otherCat.y);
			
		
			if(deltaX > deltaY)
			{
				cat.x = cat.x + 5f*Math.signum(otherCat.x - cat.x);
			}
			else
			{
				cat.y = cat.y + 5f*Math.signum(otherCat.y - cat.y);
			}
			
			double distance = Math.sqrt((cat.x - otherCat.x)*(cat.x - otherCat.x) + (cat.y - otherCat.y)*(cat.y - otherCat.y));
			
			if(distance < 0.5f)
			{
				cat.winner = true;
				otherCat.winner = false;
				response.getWriter().write("WIN");
				return;
			}
			response.getWriter().write("" + cat.x + ";" + cat.y + ";" + otherCat.x + ";" + otherCat.y);
			
			
		} catch (IOException e) {
			e.printStackTrace();
		}
    }
	
}
