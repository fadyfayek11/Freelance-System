# Freelance-System
<h1>Using Asp .net Mvc FrameWork & Entity FrameWork</h1>
<h2>Problem Definition:<h2>
<p>If you’re looking for a flexible schedule in which you can make your own hours, work from home during this pandemic, then a freelance job might be for you. A lot of work can be done remotely these days, so your dreams of working from your couch can come true by developing a freelance web application to manage all of these.
Freelance web applications are platforms, where both people looking for work and employers post their offers.</p>
<hr>
<h2>Actors (Three actors):</h2>
<ul>
<li>Admin</li>
<li>Freelancer</li>
<li>Clients</li>
<ul>

<h2>Actors Roles:</h2>
<ol>
    <li>Admin</li>
        <ul>
            <li>Control all users (Freelancers and Clients): Admin can Add, Remove Users</li>
            <li>Control the wall page: Admin can Add, Remove and Update job posts in the wall page which are created by the client</li>
            <li>Admin pages (in dashboard layout) include:</li>
            <ol>
                <li>Admin information (profile page): photo, first name, last name, email, phone number and user role (admin).</li>
                <li>All the users that using the system so that the admin can control them.</li>
                <li>All the posts that created by the clients so that the admin can control them.</li>
                <li>If the Admin remove a post from his profile it reflects directly to the wall and be removed.</li>
                <li>Admin Accept or Refuse Posts written by the Clients before adding it to the Wall.</li>
                <li>The admin must login first, to operate his job</li>
            </ol>
        <ul>    
    <li>Clients</li>
        <ul>
            <li>Write job posts that will show in the wall page</li>
            <li>Client pages (in factory layout) include:</li>
            <ol>
                <li>Client information (profile): photo, user name, first name, last name, email, phone number.</li>
                <li>Client can change (update) his information</li>
                <li>A Form that the Client will use to write job posts and add them to the Wall.</li>
                <li>Post contains: Client name, Job Type (fixed or hourly), Job Budget, post creation date, Job description, Number of proposal submitted.</li>
                <li>Admin Accept or Refuse Posts written by the Clients before adding it to the Wall.</li>
                <li>History that includes all job Posts that have been added before by this client in the Wall (mention date in each Post)</li>
                <li>Client can manage his Posts (Add, Edit, Delete, Search).</li>
                <li>Client Accept or Refuse freelancers’ proposals for a certain job. If the client accepts the proposal, the job post will be removed from the wall page.</li>               
                <li>The Client must login first, to operate his job</li>
            </ol>            
        </ul>
    <li>Freelancer</li>
    <ul>
            <li>Login to apply for a job</li>
            <li>View (read only) all job posts from different clients (no need to login)</li>
            <li>Freelancer pages (in wall layout) include:</li>
            <ol>
                <li>All Posts that have been added by Clients from their Profiles.</li>
                <li>Each post (created by Client) has: Client name, Job Type (fixed or hourly), Job Budget, post creation date, Job description, Number of proposal submitted.</li>
                <li>Freelancer can search for a job by Title, Date or Client name.</li>
                <li>If the Admin remove a post from his profile it reflects directly to the wall and be removed.</li>
                <li>Freelancer can save a particular post in saved page (to read later)</li>
                <li>User can apply to a job by sending a proposal to the client</li>
            </ol>
        <ul>    
<ol>
