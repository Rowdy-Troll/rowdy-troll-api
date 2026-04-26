Auth0 quick setup (placeholders)

This project includes minimal Auth0 scaffolding. The `Program.cs` and `Security`
helpers look for Auth0 settings in `appsettings.json` under the `Auth0` section.

1. Create an API in Auth0
   - Dashboard > Applications > APIs > Create API
   - Name: RowdyTroll API (or whatever you prefer)
   - Identifier: https://rowdy-troll-api
   - Signing Algorithm: RS256

2. Add a permission (scope)
   - In the API in Auth0, add a permission named: `delete:catalog`

3. Create a Machine-to-Machine application (or use an existing one)
   - Under the API, enable the `delete:catalog` permission for the application.

4. Set auth values in `appsettings.json` (replace placeholders)
   {
   "Auth0": {
   "Domain": "https://YOUR_DOMAIN.auth0.com/",
   "Audience": "https://rowdy-troll-api"
   }
   }

5. Test with catalog.http or curl
   - Use the Auth0 "Test" tab to get a token (cURL snippet will be provided by Auth0).
   - Use the returned `access_token` as a Bearer token to call DELETE /catalog/{id}.

Notes

- The handler expects scopes in the token under `scope` or `permissions` claims.
- This README is intentionally minimal for grading; follow Auth0 Quick Start for full details.
