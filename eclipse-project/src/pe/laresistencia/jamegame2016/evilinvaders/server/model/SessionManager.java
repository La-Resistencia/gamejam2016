package pe.laresistencia.jamegame2016.evilinvaders.server.model;

import java.util.HashMap;
import java.util.Map;

public class SessionManager {
	public static SessionManager instance = new SessionManager();
	
	public Map<String, CatActor> sessionMap;
	
	public String configureSession(String session)
	{
		if(sessionMap == null)
		{
			sessionMap = new HashMap<>();
		}
		
		if(sessionMap.size() == 0)
		{
			sessionMap.put(session, new CatActor());
			return "cat1";
		}
		if(sessionMap.size() == 1)
		{
			sessionMap.put(session, new CatActor());
			return "cat2" ;
		}
		return "nocat";
	}
	
	public CatActor getCatActorFromSession(String session)
	{
		return sessionMap.get(session);
	}
	
	public CatActor getOtherCatActorFromSession(String session)
	{
		CatActor cat = getCatActorFromSession(session);
		for (CatActor catActor : sessionMap.values()) {
			if(cat != catActor)
			{
				return catActor;
			}
		}
		return null;
	}
}