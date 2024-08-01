import { inject, injectable } from 'inversify';
import {
  HttpBackendClient,
  TYPES,
  DIContainer,
  body,
  responseJson,
} from '@socnet/shared';
import { SubscribeDeleteBody, SubscribePostBody } from './subscribe-dto';

/** /api/v1/subscribe */
@injectable()
export class SubscribeRepository {
  constructor(
    @inject(TYPES.HttpBackendClient) private _httpClient: HttpBackendClient
  ) {}

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
  DIContainer.get<SubscribeRepository>(TYPES.SubscribeRepository);
