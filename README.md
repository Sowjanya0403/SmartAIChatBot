#SMART AI CHATBOT

#Approach
-> user can ask questions through Swagger API interface.
-> For querying data from structured(SQL) and unstructured data (Blob) I have used Azure AI Search based on indexes.
-> Created a common Index for both SQL and blob files based on Roles Filter and integrated these data sources with Azure AI Search.
-> Query is searched against the indexer to find relevant docs and filtered based on Roles.
-> Finally relevant Data(Context) is passed to ChatCompletion tool which uses gpt-35-turbo Model deployed in Azue Open AI to generate responses.

#Pending Items
-> Appinsights Integration
-> Unit test Case Generation


