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
			float z = Float.parseFloat(request.getParameter("z"));
			
			CatActor cat = SessionManager.instance.getCatActorFromSession(session);
			if(x > -10000)
			{
				cat.x = x;
			}
			if(y > -10000)
			{
				cat.y = y;
			}
			cat.z = z;
			
			CatActor otherCat = SessionManager.instance.getOtherCatActorFromSession(session);
			
			if(otherCat == null)
			{
				response.getWriter().write("NIL");
				return;
			}
			
			response.getWriter().write("" + cat.x + ";" + cat.y + ";" + cat.z + ";" + otherCat.x + ";" + otherCat.y + ";" + otherCat.z);
			cat.x = -10000;
			cat.y = -10000;
			otherCat.x = -10000;
			otherCat.y = -10000;
			
			
		} catch (IOException e) {
			e.printStackTrace();
		}
    }
	
}
