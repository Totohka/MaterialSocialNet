import { SubscribeDeleteBody, SubscribePostBody } from './subscribe-dto';
import {
  body,
  getHttpBackendClient,
  HttpBackendClient,
  responseJson,
} from '@socnet/shared';

/** /api/v1/subscribe */
class SubscribeRepository {
  constructor(private _httpClient: HttpBackendClient) {}

  /** POST /api/subscribe */
  post(params: SubscribePostBody) {
    return this._httpClient
      .post('/api/v1/subscribe', body(params))
      .then(responseJson<void>);
  }

  /** DELETE /api/subscribe */
  delete(params: SubscribeDeleteBody) {
    return this._httpClient
      .delete('/api/v1/subscribe', body(params))
      .then(responseJson<void>);
  }
}

export const getSubscribeRepository = () =>
  new SubscribeRepository(getHttpBackendClient());
