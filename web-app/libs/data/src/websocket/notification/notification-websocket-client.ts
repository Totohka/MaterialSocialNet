import {
  getEnvironmentService,
  getWebsocketClient,
  WebsocketClient,
} from '@socnet/shared';
import {
  NotificationChatReturn,
  NotificationPostReturn,
  SendNotificationReturn,
} from './notification-dto';
import { NotificationConfig } from './notification-config-type';

class NotificationWebsocketClient {
  constructor(private _websocketClient: WebsocketClient) {}

  private onSendNotification(callback: (data: SendNotificationReturn) => void) {
    this._websocketClient.on('SendNotification', callback);
    return this;
  }
  private onNotificationPost(callback: (data: NotificationPostReturn) => void) {
    this._websocketClient.on('NotificationPost', callback);
    return this;
  }
  private onNotificationChat(callback: (data: NotificationChatReturn) => void) {
    this._websocketClient.on('NotificationChat', callback);
    return this;
  }

  configure({
    onNotificationChatCallback,
    onNotificationPostCallback,
    onSendNotificationCallback,
  }: NotificationConfig) {
    this._websocketClient.build();
    this.onSendNotification(onSendNotificationCallback)
      .onNotificationPost(onNotificationPostCallback)
      .onNotificationChat(onNotificationChatCallback);

    return this;
  }
  connect(userId: string) {
    return this._websocketClient
      .start()
      ?.then(() =>
        this._websocketClient.invoke('OnConnectedSendNotification', userId)
      );
  }
}

export const getNotificationWebsocketClient = () =>
  new NotificationWebsocketClient(
    getWebsocketClient(getEnvironmentService().getHubUrl().notification)
  );
