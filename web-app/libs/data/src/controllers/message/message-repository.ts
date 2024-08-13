import {
  MessageDeleteQuery,
  MessageGetListQuery,
  MessageGetListReturn,
  MessageGetReturn,
  MessagePostBody,
  MessagePutBody,
} from './message-dto';
import {
  body,
  getHttpBackendClient,
  HttpBackendClient,
  queryParams,
  responseJson,
} from '@socnet/shared';

/** /api/message */
class MessageRepository {
  constructor(private _httpClient: HttpBackendClient) {}

  /** POST /api/v1/message */
  post(payload: MessagePostBody) {
    return this._httpClient
      .post('/api/v1/message', body(payload))
      .then(responseJson<void>);
  }

  /** GET /api/v1/message/{id} */
  get(messageId: number) {
    return this._httpClient
      .get(`/api/v1/message/${messageId}`)
      .then(responseJson<MessageGetReturn>);
  }

  /** GET /api/v1/message */
  getAll(query: MessageGetListQuery) {
    return this._httpClient
      .get('/api/v1/message', queryParams(query))
      .then(responseJson<MessageGetListReturn>);
  }

  /** PUT /api/v1/message */
  put(payload: MessagePutBody) {
    return this._httpClient
      .put('/api/v1/message', body(payload))
      .then(responseJson<void>);
  }

  /** DELETE /api/v1/message/{id} */
  delete(messageId: number, query: MessageDeleteQuery) {
    return this._httpClient
      .delete(`/api/v1/message/${messageId}`, queryParams(query))
      .then(responseJson<void>);
  }
}

export const getMessageRepository = () =>
  new MessageRepository(getHttpBackendClient());
