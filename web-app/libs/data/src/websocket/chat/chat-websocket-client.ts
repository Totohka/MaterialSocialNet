import {
  getEnvironmentService,
  getWebsocketClient,
  WebsocketClient,
} from '@socnet/shared';
import {
  DeleteMessageReturn,
  ReceiveReturn,
  UpdateMessageReturn,
} from './chat-dto';
import { ChatConfig } from './chat-config-type';

class ChatWebsocketClient {
  constructor(private _websocketClient: WebsocketClient) {}

  private connectToChat(chatId: string) {
    this._websocketClient.invoke('OnConnectedChatAsync', chatId);
  }

  private onReceive(callback: (data: ReceiveReturn) => void) {
    this._websocketClient.on('Receive', callback);
    return this;
  }

  private onUpdateMessage(callback: (data: UpdateMessageReturn) => void) {
    this._websocketClient.on('UpdateMessage', callback);
    return this;
  }
  private onDeleteMessage(callback: (data: DeleteMessageReturn) => void) {
    this._websocketClient.on('DeleteMessage', callback);
    return this;
  }

  configure({
    onReceiveCallback,
    onUpdateMessageCallback,
    onDeleteMessageCallback,
  }: ChatConfig) {
    this._websocketClient.build();
    this.onReceive(onReceiveCallback)
      .onUpdateMessage(onUpdateMessageCallback)
      .onDeleteMessage(onDeleteMessageCallback);

    return this;
  }
  connect(chatIds: string[]) {
    return this._websocketClient
      .start()
      ?.then(() => chatIds.forEach(this.connectToChat))
      .catch((error) => console.error(error));
  }
}

export const getChatWebsocketClient = () =>
  new ChatWebsocketClient(
    getWebsocketClient(getEnvironmentService().getHubUrl().notification)
  );
