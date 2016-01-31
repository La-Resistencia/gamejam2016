package pe.laresistencia.jamegame2016.evilinvaders.server;

import java.util.EnumSet;

import javax.servlet.DispatcherType;

import org.eclipse.jetty.server.Handler;
import org.eclipse.jetty.server.Server;
import org.eclipse.jetty.server.handler.ContextHandlerCollection;
import org.eclipse.jetty.servlet.FilterHolder;
import org.eclipse.jetty.servlet.ServletContextHandler;
import org.eclipse.jetty.servlets.CrossOriginFilter;

import pe.laresistencia.jamegame2016.evilinvaders.server.handlers.ConfigureSessionHandler;
import pe.laresistencia.jamegame2016.evilinvaders.server.handlers.CrossdomainHandler;
import pe.laresistencia.jamegame2016.evilinvaders.server.handlers.PingHandler;
import pe.laresistencia.jamegame2016.evilinvaders.server.handlers.UpdatePositionHandler;

public class EvilInvaderServer {
	public static void main(String[] args) throws Exception {
		Server server = new Server(9117);
		
		ServletContextHandler webServicesContext = new ServletContextHandler(ServletContextHandler.SESSIONS);
        webServicesContext.setContextPath("/");
        webServicesContext.setResourceBase(System.getProperty("java.io.tmpdir"));
        server.setHandler(webServicesContext);
        
        FilterHolder cors = webServicesContext.addFilter(CrossOriginFilter.class,"/*", EnumSet.of(DispatcherType.REQUEST));
        cors.setInitParameter(CrossOriginFilter.ALLOWED_ORIGINS_PARAM, "*");
        cors.setInitParameter(CrossOriginFilter.ACCESS_CONTROL_ALLOW_ORIGIN_HEADER, "*");
        cors.setInitParameter(CrossOriginFilter.ALLOWED_METHODS_PARAM, "GET,POST,HEAD");
        cors.setInitParameter(CrossOriginFilter.ALLOWED_HEADERS_PARAM, "X-Requested-With,Content-Type,Accept,Origin");
    
        webServicesContext.addServlet(CrossdomainHandler.class, "/crossdomain.xml");
        webServicesContext.addServlet(PingHandler.class, "/ping");
        webServicesContext.addServlet(ConfigureSessionHandler.class, "/configuresession");
        webServicesContext.addServlet(UpdatePositionHandler.class, "/updateposition");
        
        ContextHandlerCollection contexts = new ContextHandlerCollection();
        contexts.setHandlers(new Handler[] { webServicesContext});
		
        server.setHandler(contexts);
        server.start();
        server.join();
	}
}
