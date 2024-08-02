import {
  HttpTransportType,
  HubConnection,
  HubConnectionBuilder,
} from '@microsoft/signalr';
import { getLocalStorage, LocalStorage } from '../storage';
import { joinUrl } from '../http';

export class WebsocketClient {
  private _hubConnection?: HubConnection;

  constructor(private _hubUrl: string, private _storage: LocalStorage) {}

  private get _hubUrlWithToken() {
    const accessToken = this._storage.getAccessToken();
    if (!accessToken) {
      throw new Error('Отсутствует accessToken');
    }
    return joinUrl(this._hubUrl, `?t=${accessToken}`);
  }

  build() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl(this._hubUrlWithToken, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
      })
      .withAutomaticReconnect()
      .build();
  }

  start() {
    return this._hubConnection?.start();
  }

  on<T>(command: string, callback: (data: T) => void) {
    this._hubConnection?.on(command, (json: string) => {
      const data = JSON.parse(json);
      callback(data);
    });
  }

  async invoke<T>(command: string, data: T) {
    await this._hubConnection?.invoke(command, data);
  }

  onKek() {
    // this._hubConnection?.onclose((error) => console.log(error));
    // this._hubConnection;
    this._hubConnection?.start();
  }

  stop() {
    this._hubConnection?.stop();
  }
}

export const getWebsocketClient = (environmentService: string) =>
  new WebsocketClient(environmentService, getLocalStorage());
