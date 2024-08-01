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
  SettingPrivacyPutBody,
  SettingNotificationPutBody,
} from './setting-dto';

/** /api/v1/setting */
@injectable()
export class SettingRepository {
  constructor(
    @inject(TYPES.HttpBackendClient) private _httpClient: HttpBackendClient
  ) {}

  /** PUT /api/v1/setting/privacy */
  putPrivacy(params: SettingPrivacyPutBody) {
    return this._httpClient
      .post('/api/v1/setting/privacy', body(params))
      .then(responseJson<string>);
  }

  /** PUT /api/v1/setting/privacy */
  putNotification(params: SettingNotificationPutBody) {
    return this._httpClient
      .post('/api/v1/setting/notification', body(params))
      .then(responseJson<string>);
  }
}

export const getSettingRepository = () =>
  DIContainer.get<SettingRepository>(TYPES.SettingRepository);
