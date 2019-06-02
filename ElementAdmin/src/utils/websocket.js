import * as signalR from "@aspnet/signalr";



export async function connection(hub) {
  var connection = new signalR.HubConnectionBuilder().withUrl(`${process.env.VUE_APP_BASE_API}/${hub}`).configureLogging(signalR.LogLevel.Error).build();
  await connection.start();
  var result = {
    connection,
    id: ""
  };
  connection.on("GetConnectedId", (connectedid) => {
    result.id = connectedid
    console.log(`connectedId:${connectedid}`);
  });
  return result;
}
