<h2>WPF-WCF-Communication</h2>

<h3>Senario covered</h3>
<img src="images/task.jpg">

<h3>Tools, Framework & Environment used</h3>
	<ul>
		<li>Windows 10</li>
		<li>Visual Studio Community Edition 2019 (v.-16.1.1)</li>
		<li>.NET Framework 4.7.2</li>
		<li>IIS (v.-10)</li>
	</ul>

<h3>Client-Server communication flow</h3>
<p>I have implemented 2 approaches to communicate from client (WPF) to server (WCF).</p>
<ol>
	<li>Communication using WCF REST</li>
	<li>Communication using Service Reference</li>
</ol>

<img src="images/communication_flow.jpg" width="60%" height="70%">


<h3>Explanation on WcfServer</h3>
<ol>
	<li>
  	<ul>
		  <li>Added rest-endpoint and rest-behaviour in the web.config file to enable REST request-response.</li>
		  <li>If we only have rest-endpoint in WCF, ServiceReference in WPF will not work as REST does not pass over any metadata.</li>
		  <li>To solve this, soap-endpoint is added here in WCF. (App.config file in WPF will have this endpoint)</li>
		  <img src="images/rest_soap_endpoint.jpg">
      <img src="images/rest_endpoint_behaviour.jpg" width="35%" height="45%">
	  </ul>
  </li>
	<li>
    <ul>
		  <li>In order to expose the service, “WebInvoke” attribute has been used in IWcfServer.cs interface. Here, URI Template defines the URL format by which this method is identified / linked.</li>
      <img src="images/WebInvoke.jpg">
    </ul>
  </li>
  	<li>
    <ul>
		  <li>OutgoingResponseFormat.cs class has been used to dynamically send the response either in json or xml format according to client needs.</li>
    </ul>
  </li>
</ol>

