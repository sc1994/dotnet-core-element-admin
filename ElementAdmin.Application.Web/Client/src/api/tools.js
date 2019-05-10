import request from '@/utils/request'

export function initRoutesData(data) {
  return request({
    url: '/tools/initroutedata',
    method: 'post',
    data
  })
}
