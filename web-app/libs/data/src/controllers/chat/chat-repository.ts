import { inject, injectable } from 'inversify';
import {
  HttpBackendClient,
  TYPES,
  DIContainer,
  body,
  queryParams,
  responseJson,
} from '@socnet/shared';
import {
  ChatGetListQuery,
  ChatGetListReturn,
  ChatGetReturn,
  ChatPostBody,
  ChatPutBody,
} from './chat-dto';

/** /api/chat */
@injectable()
export class ChatRepository {
  constructor(
    @inject(TYPES.HttpBackendClient) private _httpClient: HttpBackendClient
  ) {}

  /** POST /api/v1/chat */
  post(payload: ChatPostBody) {
    return this._httpClient
      .post('/api/v1/chat', body(payload))
      .then(responseJson<number>);
  }

  /** GET /api/v1/chat */
  get(query: ChatGetListQuery) {
    return this._httpClient
      .get('/api/v1/chat', queryParams(query))
      .then(responseJson<ChatGetListReturn>);
  }

  /** GET /api/v1/chat/{id} */
  getById(chatId: number) {
    return this._httpClient
      .get(`/api/v1/chat/${chatId}`)
      .then(responseJson<ChatGetReturn>);
  }

  /** PUT /api/v1/chat */
  put(payload: ChatPutBody) {
    return this._httpClient
      .put('/api/v1/chat', body(payload))
      .then(responseJson<void>);
  }
  /** DELETE /api/v1/chat/{id} */
  delete(chatId: number) {
    return this._httpClient
      .delete(`/api/v1/chat/${chatId}`)
      .then(responseJson<void>);
  }
}

export const getChatRepository = () =>
  DIContainer.get<ChatRepository>(TYPES.UserRepository);
